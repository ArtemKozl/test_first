using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Text.Json;
using test_first.API.Contracts;
using test_first.Application.Services;

namespace test_first.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class DeliveryController : Controller
    {
        private readonly IDeliveryService _deliveryService;
        private readonly ILogger<DeliveryController> _logger;
        public DeliveryController(IDeliveryService deliveryService, ILogger<DeliveryController> logger)
        {
            _deliveryService = deliveryService;
            _logger = logger;
        }


        // Эндпоинт от старого задания. Сортировка считает количество всех заказов в каждом районе, сравнивает с числом необходимых заказов (> || <),
        // и сортирует по заданными временным рамкам (от и до)
        [HttpPost("MoreOrLess")]
        public async Task<IActionResult> GetDeliveriesMoreOrLessCondition([FromBody] DeliveryMoreOrLessRequest request)
        {
            try
            {
                var result = await _deliveryService.GetDeliveries(request.numberOfDeliveries, request.conditionMoreOrLess,
                    request.startDate, request.endDate);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Произошла ошибка при обработке запроса GetDeliveries: {JsonSerializer.Serialize(request)}");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        // Эндпоинт от нового задания. сортирует по переданному району и интервалу от заданного первого заказа и его же, но +30 мин
        [HttpPost("FirstDelivery")]
        public async Task<IActionResult> GetDeliveriesFirstDeliveryCondition([FromBody] DeliveryFirstDeliveryRequest request)
        {
            try
            {
                var result = await _deliveryService.GetDeliveries(request.districtName, request.startDate);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Произошла ошибка при обработке запроса GetDeliveries: {JsonSerializer.Serialize(request)}");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }
    }
}
