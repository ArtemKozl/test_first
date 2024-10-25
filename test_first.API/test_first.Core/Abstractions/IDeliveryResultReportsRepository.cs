using System.Text.Json;

namespace test_first.DataAccess.Repositories
{
    public interface IDeliveryResultReportsRepository
    {
        Task AddReportToDb(JsonDocument deliveryReport);
    }
}