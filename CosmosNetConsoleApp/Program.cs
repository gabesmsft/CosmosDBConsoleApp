using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Azure.Cosmos;
using System.Configuration;
using System.Collections.Generic;

namespace CosmosNetConsoleApp
{
    public class Program
    {
        // The Azure Cosmos DB endpoint for running this sample.
        private static readonly string EndpointUri = ConfigurationManager.AppSettings["EndpointUri"];
        // The primary key for the Azure Cosmos account.
        private static readonly string PrimaryKey = ConfigurationManager.AppSettings["PrimaryKey"];

        private Database database;

        private Container container;

        static string databaseId = ConfigurationManager.AppSettings["databaseId"];
        static string containerId = ConfigurationManager.AppSettings["containerId"];

        CosmosClient cosmosClient;

        public static async Task Main(string[] args)
        {
            Program p = new Program();
            await p.InitializeAsync();
            
            Console.Write("Press 1 to read all items from collection, or press 2 to add an item to the collection:\n");
            string choice= Console.ReadLine();
            if (choice == "1")
                await p.QueryItemsAsync();
            else if (choice == "2")
                await p.AddItemsToContainerAsync();
            else
                Console.Write("Neither 1 nor 2 were pressed, application exited.");
        }

        public async Task InitializeAsync()
        {
            cosmosClient = new CosmosClient(EndpointUri, PrimaryKey);
            container = cosmosClient.GetContainer(databaseId, containerId);
        }

        private async Task AddItemsToContainerAsync()
        {
            Console.Write("Document Id:");
            var idInput = Console.ReadLine();
            Console.Write("Document property:");
            var somePropertyInput = Console.ReadLine();
            someEntity e = new someEntity
            {
                Id = idInput,
                someProperty = somePropertyInput

            };
            ItemResponse<someEntity> eResponse = await  container.CreateItemAsync<someEntity>(e, new PartitionKey(e.someProperty));
        }

        private async Task QueryItemsAsync()
        {
            var sqlQueryText = "SELECT * FROM c";

            Console.WriteLine("Running query: {0}\n", sqlQueryText);

            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            FeedIterator<someEntity> queryResultSetIterator = container.GetItemQueryIterator<someEntity>(queryDefinition);

            List<someEntity> entities = new List<someEntity>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<someEntity> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (someEntity entity in currentResultSet)
                {
                    entities.Add(entity);
                    Console.WriteLine("\tRead {0}\n", entity);
                }
            }
        }
    }

    public class someEntity
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

		//replace with your partition name
        [JsonProperty(PropertyName = "fakepar")]
        public string someProperty { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
