using Cognizant.Training.StorageAccount.API.Model;
using Cognizant.Training.StorageAccount.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Cognizant.Training.StorageAccount.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlobController : ControllerBase
    { 
        private readonly BlobRepository _repository;

        public BlobController(BlobRepository repository)
        {
            _repository = repository;
        }

        [HttpGet()]
        public async Task<IEnumerable<string>> GetAll()
        {
            return await _repository.GetBlobs();
        }          

        [HttpPost()]
        public async Task<IActionResult> Add(string name)
        {
            await _repository.AddBlob(name);
            return Ok();
        }
    }
}
