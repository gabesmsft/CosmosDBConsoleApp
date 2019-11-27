using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Azure.Cosmos;
using System.Configuration;

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
            await p.AddItemsToContainerAsync();
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
