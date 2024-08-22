using Abdt.ContractBroker.Domain;
using Abdt.ContractBroker.Repository.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Abdt.ContractBroker.Controllers
{
    [ApiController]
    [Route("api/v1/contract")]
    public class ContractController : ControllerBase
    {
        private readonly IRepository<Contract> _repository;

        public ContractController(IRepository<Contract> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> SaveContract([FromBody] Contract contract)
        {
            if (contract is null)
                return BadRequest("Invalid data");

            await _repository.Add(contract);
            return Ok();
        }

        [HttpGet]
        [Route("get/{assemblyName}")]
        public async Task<IActionResult> GetContract([FromRoute] string assemblyName)
        {
            if (string.IsNullOrWhiteSpace(assemblyName))
                return BadRequest("Invalid data");

            var contract = await _repository.GetByKey(assemblyName);
            return Ok(contract);
        }
    }
}
