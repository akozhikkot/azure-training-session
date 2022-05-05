using Microsoft.AspNetCore.Mvc;
using RetailApp.API.Repository;
using RetailApp.Domain;

namespace RetailApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController :  ControllerBase
    {
        private readonly OrderRepository _repository; 

        public OrdersController(OrderRepository repository)
        {
            _repository = repository; 
        }

        [HttpPost()]
        public async Task<IActionResult> Create(Order order)
        {
            var result = await _repository.Create(order);
            return Ok(result);
        }

        [HttpPost("bulk/{start}")]
        public async Task<IActionResult> Create(int start)
        {
            await _repository.BulkInsert(start);
            return Ok();
        }

        [HttpGet()]
        public async Task<IActionResult> GetByName([FromQuery]string name)
        {
            var results = await _repository.Query(name);
            return Ok(results);
        }
    }
}
