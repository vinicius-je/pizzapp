using Microsoft.AspNetCore.Mvc;
using PizzApp.Application.Features.OrderFeatures.Create;

namespace PizzApp.Presentation.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : Controller
    {
        public readonly ICreateOrderService _createOrderService;

        public OrderController(ICreateOrderService createOrderService)
        {
            _createOrderService = createOrderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request, CancellationToken cancellationToken)
        {
            var result = await _createOrderService.Execute(request, cancellationToken);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Created($"api/orders/{result.ResourceId}", null);
        }

    }
}
