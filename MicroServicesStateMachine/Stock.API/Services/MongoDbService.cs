using System;
using MongoDB.Driver;

namespace Stock.API.Services
{
	public sealed class MongoDbService
	{
		private readonly IMongoDatabase _database;

		public MongoDbService(IConfiguration configuration)
		{
			MongoClient client = new MongoClient(configuration.GetConnectionString("MongoDB"));
			_database = client.GetDatabase("StockOrchDB");
		}
		public IMongoCollection<T> GetCollection<T>() => _database.GetCollection<T>(typeof(T).Name.ToLowerInvariant());


	}
}

