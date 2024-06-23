using HistoricosApp.API.Models;
using HistoricosApp.API.Settings;
using MongoDB.Driver;

namespace HistoricosApp.API.Contexts
{
    public class MongoDBContext
    {
        private readonly MongoDBSettings? _mongoDBSettings;
        private IMongoDatabase _database;

        public MongoDBContext(MongoDBSettings? mongoDBSettings)
        {
            _mongoDBSettings = mongoDBSettings;

            #region Conexão com o MongoDB

            var mongoClientSettings = MongoClientSettings.FromUrl(new MongoUrl(_mongoDBSettings?.Host));
            var mongoClient = new MongoClient(mongoClientSettings);
            _database = mongoClient.GetDatabase(_mongoDBSettings?.Database);

            #endregion
        }

        #region Mapeamento da collection que irá gravar o log de produtos

        public IMongoCollection<LogProdutos> Produtos 
            => _database.GetCollection<LogProdutos>("LogProdutos");

        #endregion
    }
}
