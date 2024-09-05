using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Abdt.ContractBroker.Domain
{
    public class Contract
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string AssemblyName { get; set; } = string.Empty;

        [BsonElement("data")]
        public BsonDocument? BsonData { get; set; }
    }
}
