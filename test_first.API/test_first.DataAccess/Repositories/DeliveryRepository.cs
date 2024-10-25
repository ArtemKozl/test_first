using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using test_first.Core.Models;

namespace test_first.DataAccess.Repositories
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly PostgreDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<DeliveryRepository> _logger;
        public DeliveryRepository(PostgreDbContext context, IMapper mapper, ILogger<DeliveryRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string[]> GetDistrictsArrayByCondition(int numberOfDeliveries, char condition)
        {
            _logger.LogInformation($"Начало GetDistinctsArrayByCondition: condition: {condition}, numberOfDeliveries: {numberOfDeliveries}");

            IQueryable<string> districtsNamesQuery;

            try
            {
                if (condition == '>')
                {
                    districtsNamesQuery = _context.Deliveries
                        .GroupBy(x => x.district)
                        .Where(g => g.Count() > numberOfDeliveries)
                        .Select(g => g.Key);
                }
                else
                {
                    districtsNamesQuery = _context.Deliveries
                        .GroupBy(x => x.district)
                        .Where(g => g.Count() < numberOfDeliveries)
                        .Select(g => g.Key);
                }

                var districtNames = await districtsNamesQuery.ToArrayAsync();

                _logger.LogInformation($"Завершение GetDistinctsArrayByCondition. Возращено {districtNames.Length} имен районов.");
                return districtNames;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка GetDistinctsArrayByCondition: {ex.Message}", ex);
                throw;
            }
        }


        public async Task<List<Deliveries>> GetDeliveriesByConditions(string[] districtsNames, DateTime startWith, DateTime endWith)
        {
            _logger.LogInformation($"Начало GetDeliveriesByConditions с районами: {string.Join(", ", districtsNames)}, startWith: {startWith}, endWith: {endWith}");

            try
            {
                var result = await _context.Deliveries
                    .Where(d =>
                        districtsNames.Contains(d.district) &&
                        d.delivery_date >= startWith &&
                        d.delivery_date <= endWith)
                    .ToListAsync();

                _logger.LogInformation($"Завершение GetDeliveriesByConditions. Получено {result.Count} доставок.");
                return _mapper.Map<List<Deliveries>>(result);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Ошибка базы данных произошла в GetDeliveriesByConditions: {ex.Message}", ex);
                throw new InvalidDataException("Не удалось получить доставки из базы данных", ex);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError($"Ошибка недопустимого параметра в GetDeliveriesByConditions: {ex.Message}", ex);
                throw new ArgumentException("Недопустимые названия районов или диапазон дат", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Неожиданная ошибка в GetDeliveriesByConditions: {ex.Message}", ex);
                throw new ApplicationException("При получении доставок произошла непредвиденная ошибка", ex);
            }
        }

        public async Task<List<Deliveries>> GetDeliveriesByConditions(string districtName, DateTime firstDeliveryDateTime)
        {
            _logger.LogInformation($"Начало GetDeliveriesByConditions с районом: {districtName}, начальное время: {firstDeliveryDateTime}");

            try
            {
                var result = await _context.Deliveries
                    .Where(g => g.district == districtName)
                    .Where(d => d.delivery_date >= firstDeliveryDateTime && d.delivery_date <= firstDeliveryDateTime.AddMinutes(30))
                    .OrderByDescending(d => d.delivery_date)
                    .ToListAsync();

                _logger.LogInformation($"Завершение GetDeliveriesByConditions. Получено {result.Count} доставок.");
                return _mapper.Map<List<Deliveries>>(result);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Ошибка базы данных произошла в GetDeliveriesByConditions: {ex.Message}", ex);
                throw new InvalidDataException("Не удалось получить доставки из базы данных", ex);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError($"Ошибка недопустимого параметра в GetDeliveriesByConditions: {ex.Message}", ex);
                throw new ArgumentException("Недопустимые названия районов или диапазон дат", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Неожиданная ошибка в GetDeliveriesByConditions: {ex.Message}", ex);
                throw new ApplicationException("При получении доставок произошла непредвиденная ошибка", ex);
            }
        }
    }
}
