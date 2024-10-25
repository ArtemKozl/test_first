
namespace test_first.DataAccess.Entities
{
    public class DeliveryEntity
    {
        public int id { get; set; }
        public string district { get; set; } = string.Empty;
        public int weight { get; set; }
        public DateTime delivery_date { get; set; }
    }
}
