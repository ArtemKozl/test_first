using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using test_first.DataAccess.Entities;

namespace test_first.DataAccess
{
    public class PostgreDbContext : DbContext
    {
        private readonly ILogger _logger;

        public PostgreDbContext(DbContextOptions<PostgreDbContext> options, ILoggerFactory loggerFactory)
            : base(options)
        {
            _logger = loggerFactory.CreateLogger<PostgreDbContext>();
        }

        public DbSet<DeliveryEntity> Deliveries { get; set; }
        public DbSet<DeliveryResultPeportEntity> DeliveryResultReports { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            _logger.LogInformation("Подключение к базе данных PostgreSQL успешно установлено.");
        }
    }
}
