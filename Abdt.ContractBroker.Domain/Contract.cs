using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Abdt.ContractBroker.Domain
{
    public class Contract
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string FullAssemblyName { get; set; } = string.Empty;

        [BsonElement("data")]
        public string JsonData { get; set; } = string.Empty;
    }
}
