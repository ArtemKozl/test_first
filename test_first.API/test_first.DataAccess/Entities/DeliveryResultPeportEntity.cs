using System.Text.Json;

namespace test_first.DataAccess.Entities
{
    public class DeliveryResultPeportEntity
    {
        public int id { get; set; }
        public DateTime report_date { get; set; }
        public JsonDocument? result_report { get; set; }
    }
}
