using test_first.Core.Models;

namespace test_first.DataAccess.Repositories
{
    public interface IDeliveryRepository
    {
        Task<List<Deliveries>> GetDeliveriesByConditions(string districtName, DateTime firstDeliveryDateTime);
        Task<List<Deliveries>> GetDeliveriesByConditions(string[] districtsNames, DateTime startWith, DateTime endWith);
        Task<string[]> GetDistrictsArrayByCondition(int numberOfDeliveries, char condition);
    }
}