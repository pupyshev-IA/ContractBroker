using Abdt.ContractBroker.Domain;
using Abdt.ContractBroker.DTO;
using AutoMapper;
using MongoDB.Bson;
using System.Text.Json;

namespace Abdt.ContractBroker.Configuration
{
    public class ControllersMappingProfile : Profile
    {
        public ControllersMappingProfile()
        {
            CreateMap<ContractDto, Contract>()
                .ForMember(d => d.AssemblyName, opt => opt.MapFrom(s => GetAssemblyName(s.JsonData)))
                .ForMember(d => d.BsonData, opt => opt.MapFrom(s => BsonDocument.Parse(s.JsonData.ToString())));

            CreateMap<Contract, ContractDto>()
                .ForMember(d => d.JsonData, opt => opt.MapFrom(s => ConvertBsonToJsonElement(s.BsonData)));
        }

        private JsonElement ConvertBsonToJsonElement(BsonDocument bsonDoc)
        {
            var jsonString = bsonDoc.ToJson();
            var jsonElement = JsonDocument.Parse(jsonString).RootElement.Clone();

            return jsonElement;
        }

        private string? GetAssemblyName(JsonElement json) => 
            json.GetProperty(nameof(Contract.AssemblyName)).GetString();
    }
}
