using Microsoft.Extensions.Logging;
using System.Text.Json;
using test_first.DataAccess.Entities;

namespace test_first.DataAccess.Repositories
{
    public class DeliveryResultReportsRepository : IDeliveryResultReportsRepository
    {
        private readonly PostgreDbContext _context;
        private readonly ILogger<DeliveryResultReportsRepository> _logger;

        public DeliveryResultReportsRepository(PostgreDbContext context, ILogger<DeliveryResultReportsRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddReportToDb(JsonDocument deliveryReport)
        {
            DeliveryResultPeportEntity report = new DeliveryResultPeportEntity
            {
                report_date = DateTime.UtcNow,
                result_report = deliveryReport
            };

            try
            {
                _logger.LogInformation($"Добавление нового отчета о доставке: {report.result_report}");

                await _context.DeliveryResultReports.AddAsync(report);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Отчет успешно добавлен в базу данных");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при добавлении отчета о доставке: {ex.Message}");
                throw;
            }
        }
    }
}
