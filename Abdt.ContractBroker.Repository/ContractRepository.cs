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
        private readonly IMongoCollection<Contract> _collection;

        public ContractRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _collection = database.GetCollection<Contract>(settings.Value.CollectionName);
        }

        public async Task Add(Contract item)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));
            await _collection.InsertOneAsync(item);
        }

        public async Task<Contract?> GetByKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException(string.Format(EmptySpaceErrorMessage, nameof(key)));

            return await _collection.Find(item => item.FullAssemblyName == key).FirstOrDefaultAsync();
        }
    }
}
