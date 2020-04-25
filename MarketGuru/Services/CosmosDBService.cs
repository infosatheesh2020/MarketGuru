using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketGuru.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Configuration;

namespace MarketGuru.BusinessLogic
{
    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;

        public CosmosDbService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }
        
        public async Task AddItemAsync(Quote item)
        {
            await this._container.CreateItemAsync<Quote>(item, new PartitionKey(item.Id));
        }

        
    }
}
