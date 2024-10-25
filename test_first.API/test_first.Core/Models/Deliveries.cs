
namespace test_first.Core.Models
{
    public class Deliveries
    {

        public int Id { get; set; }
        public string District { get; set; } = string.Empty;
        public int Weight { get; set; }
        public DateTime Delivery_date { get; set; }

    }
}
