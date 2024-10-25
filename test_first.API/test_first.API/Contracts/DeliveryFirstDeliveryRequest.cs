using System.ComponentModel.DataAnnotations;

namespace test_first.API.Contracts
{
    public record DeliveryFirstDeliveryRequest(
        [Required] string districtName,
        [Required] string startDate);
        
    
}
