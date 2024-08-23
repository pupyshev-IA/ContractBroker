using Abdt.ContractBroker.Domain;
using Abdt.ContractBroker.DTO;
using AutoMapper;
using MongoDB.Bson;

namespace Abdt.ContractBroker.Configuration
{
    public class ControllersMappingProfile : Profile
    {
        public ControllersMappingProfile()
        {
            CreateMap<ContractDto, Contract>()
                .ForMember(d => d.FullAssemblyName, opt => opt.MapFrom(s => s.FullAssemblyName))
                .ForMember(d => d.BsonData, opt => opt.MapFrom(s => BsonDocument.Parse(s.JsonData)));

            CreateMap<Contract, ContractDto>()
                .ForMember(d => d.FullAssemblyName, opt => opt.MapFrom(s => s.FullAssemblyName))
                .ForMember(d => d.JsonData, opt => opt.MapFrom(s => ConvertBsonToJsonString(s.BsonData)));
        }

        private string ConvertBsonToJsonString(BsonDocument bsonDoc) => bsonDoc.ToJson();
    }
}
