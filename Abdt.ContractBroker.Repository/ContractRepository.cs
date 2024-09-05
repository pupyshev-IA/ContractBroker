using Abdt.ContractBroker.Domain;
using Abdt.ContractBroker.Domain.Options;
using Abdt.ContractBroker.Repository.Abstractions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Abdt.ContractBroker.Repository
{
    public class ContractRepository : IRepository<Contract>
    {
        private const string EmptySpaceErrorMessage = "Parameter {0} cannot be null or empty or white space";
        private readonly IMongoCollection<Contract> _contractCollection;

        public ContractRepository(IOptions<MongoDbSettings> settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _contractCollection = database.GetCollection<Contract>(settings.Value.CollectionName);
        }

        public async Task Add(Contract item)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));

            var filter = Builders<Contract>.Filter.Eq(x => x.AssemblyName, item.AssemblyName);
            var options = new ReplaceOptions { IsUpsert = true};

            await _contractCollection.ReplaceOneAsync(filter, item, options);
        }

        public async Task<Contract?> GetByKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException(string.Format(EmptySpaceErrorMessage, nameof(key)));

            return await _contractCollection.Find(item => item.AssemblyName == key).FirstOrDefaultAsync();
        }
    }
}
