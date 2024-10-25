using test_first.Core.Models;

namespace test_first.Application.Services
{
    public interface IDeliveryService
    {
        Task<List<Deliveries>> GetDeliveries(int numberOfDeliveries, char conditionMoreOrLess, string startDate, string endDate);
        Task<List<Deliveries>> GetDeliveries(string disreictName, string DeliveryDateTime);
    }
}