using Cognizant.Training.StorageAccount.API.Model;
using Cognizant.Training.StorageAccount.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Cognizant.Training.StorageAccount.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : ControllerBase
    { 
        private readonly CustomerQueueRepository _repository;

        public MessagesController(CustomerQueueRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("receiveall")]
        public IEnumerable<string> GetAll()
        {
            return _repository.GetMessages();
        }
        
        [HttpGet("peekall")]
        public IEnumerable<string> PeekAll()
        {
            return _repository.GetMessages(true);
        }        

        [HttpPost()]
        public IActionResult Add(string message)
        {
            _repository.AddMessage(message);
            return Ok();
        }
    }
}
