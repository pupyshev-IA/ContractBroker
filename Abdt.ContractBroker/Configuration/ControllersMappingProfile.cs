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
                .ForMember(d => d.FullAssemblyName, opt => opt.MapFrom(s => s.FullAssemblyName))
                .ForMember(d => d.BsonData, opt => opt.MapFrom(s => BsonDocument.Parse(s.JsonData.ToString())));

            CreateMap<Contract, ContractDto>()
                .ForMember(d => d.FullAssemblyName, opt => opt.MapFrom(s => s.FullAssemblyName))
                .ForMember(d => d.JsonData, opt => opt.MapFrom(s => ConvertBsonToJsonElement(s.BsonData)));
        }

        private JsonElement ConvertBsonToJsonElement(BsonDocument bsonDoc)
        {
            var jsonString = bsonDoc.ToJson();
            var jsonElement = JsonDocument.Parse(jsonString).RootElement.Clone();

            return jsonElement;
        }
    }
}
