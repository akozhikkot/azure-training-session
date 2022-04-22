using Cognizant.Training.StorageAccount.API.Model;
using Cognizant.Training.StorageAccount.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Cognizant.Training.StorageAccount.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    { 
        private readonly CustomerTableStorageRepository _repository;

        public CustomersController(CustomerTableStorageRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{partitionKey}")]
        public IEnumerable<Customer> Get(string partitionKey)
        {
            return _repository.GetAll(partitionKey);
        }

        [HttpPost()]
        public IActionResult Add(Customer customer)
        {
            _repository.AddCustomer(customer);
            return Ok();
        }
    }
}
