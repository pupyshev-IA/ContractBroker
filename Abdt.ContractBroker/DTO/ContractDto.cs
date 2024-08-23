using System.Text.Json;

namespace Abdt.ContractBroker.DTO
{
    public class ContractDto
    {
        public string FullAssemblyName { get; set; } = string.Empty;

        public JsonElement JsonData { get; set; }
    }
}
