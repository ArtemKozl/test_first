using System.ComponentModel.DataAnnotations;

namespace test_first.API.Contracts
{
    public record DeliveryMoreOrLessRequest(
        [Required] int numberOfDeliveries,
        [Required] char conditionMoreOrLess,
        [Required] string startDate,
        [Required] string endDate);
    
}
