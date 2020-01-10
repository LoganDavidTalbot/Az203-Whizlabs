using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosSQL
{
    class Program
    {
        private static string endpointUrl = "https://whizlabs-cosmos.documents.azure.com:443/";
        private static string authorizationKey = "8QXK8d4Emzjl3s942htQP1k6luUKaHjKdpfTn7BEZk7WfT6ne7m6do1kZ0lGtAj62ipcA9sOWiLyDvqbIU2IhQ==";
        private static DocumentClient client;
        private static Database database;
        static void Main(string[] args)
        {
            //RunDatabaseDemo().Wait();
            //RunCollectionDemo().Wait();
            //CreateDocument().Wait();
            //QueryDocument().Wait();
            //UpdateDocument().Wait();
            DeleteDocument().Wait();
            Console.ReadKey();
        }

        private static async Task RunDatabaseDemo()
        {
            using (client = new DocumentClient(new Uri(endpointUrl), authorizationKey))
            {
                database = await client.CreateDatabaseAsync(new Database { Id = "WhizlabsDB2" });
            }
            Console.WriteLine("Database Created");
        }

        private static async Task RunCollectionDemo()
        {
            using (client = new DocumentClient(new Uri(endpointUrl), authorizationKey))
            {
                database = await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri("WhizlabsDB2"));
                DocumentCollection c1 = await client.CreateDocumentCollectionAsync(database.SelfLink, new DocumentCollection { Id = "Customer" });
            }
            Console.WriteLine("Collection Created");
        }

        private static async Task CreateDocument()
        {
            Customer obj = new Customer("1", "John", "john@go.com");
            using (client = new DocumentClient(new Uri(endpointUrl), authorizationKey))
            {
                await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri("WhizlabsDB2", "Customer"), obj);
            }
            Console.WriteLine("Document Added");
        }

        private static async Task DeleteDocument()
        {
            using (client = new DocumentClient(new Uri(endpointUrl), authorizationKey))
            {
                await client.DeleteDocumentAsync(UriFactory.CreateDocumentUri("WhizlabsDB2", "Customer", "1"));
            }
            Console.WriteLine("Document deleted");
        }

        private static async Task UpdateDocument()
        {
            Customer obj = new Customer("1", "Mark", "mark@go.com");
            using (client = new DocumentClient(new Uri(endpointUrl), authorizationKey))
            {
                await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri("WhizlabsDB2", "Customer", "1"), obj);
            }
            Console.WriteLine("Document Updated");
        }

        private static async Task QueryDocument()
        {
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };
            using (client = new DocumentClient(new Uri(endpointUrl), authorizationKey))
            {
                IQueryable<Customer> customerQuery = client.CreateDocumentQuery<Customer>(
                        UriFactory.CreateDocumentCollectionUri("WhizlabsDB2", "Customer"), queryOptions)
                    .Where(c => c.CustomerId == "1");

                foreach (var customer in customerQuery)
                {
                    Console.WriteLine($"{customer.CustomerId}, {customer.CustomerName}, {customer.CustomerEmail}");
                }
            }
            Console.WriteLine("Query Complete");
        }
    }
}
