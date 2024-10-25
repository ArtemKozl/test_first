using Microsoft.Extensions.Logging;
using System.Text.Json;
using test_first.Core.Models;
using test_first.DataAccess.Repositories;
using test_first.Infrastructure;

namespace test_first.Application.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly IDeliveryResultReportsRepository _resultReportsRepository;
        private readonly IInputDataValidator _inputDataValidator;
        private readonly ILogger<DeliveryService> _logger;

        public DeliveryService(IDeliveryRepository deliveryRepository, IDeliveryResultReportsRepository resultReportsRepository,
            IInputDataValidator inputDataValidator, ILogger<DeliveryService> logger)
        {
            _deliveryRepository = deliveryRepository;
            _resultReportsRepository = resultReportsRepository;
            _inputDataValidator = inputDataValidator;
            _logger = logger;
        }

        public async Task<List<Deliveries>> GetDeliveries(int numberOfDeliveries, char conditionMoreOrLess,
            string startDate, string endDate)
        {
            try
            {
                var validatedData = _inputDataValidator.ValidateAllInputData(conditionMoreOrLess, startDate, endDate);

                string[] districts = await _deliveryRepository.GetDistrictsArrayByCondition(numberOfDeliveries, validatedData.ConditionMoreOrLessValidated);

                var result = await _deliveryRepository.GetDeliveriesByConditions(districts,
                    validatedData.StartDateValidated, validatedData.EndDateValidated);

                await _resultReportsRepository.AddReportToDb(JsonDocument.Parse(JsonSerializer.Serialize(result)));

                _logger.LogInformation("Валидация входных данных прошла успешно");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting a report");
                throw;
            }
        }

        public async Task<List<Deliveries>> GetDeliveries(string disreictName, string DeliveryDateTime)
        {
            try
            {
                var validatedData = _inputDataValidator.ValidateAllInputData(disreictName, DeliveryDateTime);

                var result = await _deliveryRepository.GetDeliveriesByConditions(validatedData.DistrictName, validatedData.DateValidated);

                await _resultReportsRepository.AddReportToDb(JsonDocument.Parse(JsonSerializer.Serialize(result)));

                _logger.LogInformation("Валидация входных данных прошла успешно");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting a report");
                throw;
            }
        }
    }
}
