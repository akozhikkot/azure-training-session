using Cognizant.Training.OrderProcessing.API.Dto;
using Cognizant.Training.OrderProcessing.API.UseCases.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Cognizant.Training.OrderProcessing.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly ICreateOrder _createOrder;
        private readonly IGetOrderById _getOrderById;

        public OrdersController(ICreateOrder createOrder, IGetOrderById getOrderById)
        {
            _createOrder = createOrder;
            _getOrderById = getOrderById;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto request, CancellationToken token = default)
        {
            var orderId = await _createOrder.Execute(request, token);
            return Created($"/orders/{orderId}", null);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById([FromRoute] string id, CancellationToken token = default)
        {
            var orderItem = await _getOrderById.Execute(id, token);

            if (orderItem == null)
            {
                return NotFound();
            }

            return Ok(orderItem);
        }
    }
}
