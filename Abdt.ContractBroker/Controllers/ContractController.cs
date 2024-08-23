using Abdt.ContractBroker.Domain;
using Abdt.ContractBroker.DTO;
using Abdt.ContractBroker.Repository.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Abdt.ContractBroker.Controllers
{
    [ApiController]
    [Route("api/v1/contract")]
    public class ContractController : ControllerBase
    {
        private readonly IRepository<Contract> _repository;
        private readonly IMapper _mapper;

        public ContractController(IRepository<Contract> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> SaveContract([FromBody] ContractDto contractRequest)
        {
            var contract = _mapper.Map<Contract>(contractRequest);
            await _repository.Add(contract);

            return Ok();
        }

        [HttpGet]
        [Route("get/{assemblyName}")]
        public async Task<IActionResult> GetContract([FromRoute] string assemblyName)
        {
            var contract = await _repository.GetByKey(assemblyName);
                if (contract == null)
                    return NotFound();

            var contractResponse = _mapper.Map<ContractDto>(contract);
            return Ok(contractResponse);
        }
    }
}
