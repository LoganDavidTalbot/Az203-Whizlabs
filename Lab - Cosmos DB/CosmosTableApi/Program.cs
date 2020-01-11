using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosTableApi
{
    class Program
    {
        static string cosmosdb_conn_string = "DefaultEndpointsProtocol=https;AccountName=whizlabstabledb;AccountKey=tFMRKDf6jPkfI8oQA36l8YIHq8G0Aw4a4dy2NKGuci2Qc4s0Di34CZRUQsL4TijiI5EttGx92jvfWRZl5wyp3A==;TableEndpoint=https://whizlabstabledb.table.cosmos.azure.com:443/;";
        static string azuretable_conn_string = "DefaultEndpointsProtocol=https;AccountName=whizlabsstore;AccountKey=LPHKW9AAxAVo5Oro3ue4EuWkfHNARLHt0YYq4etLhy/QTE+c7MK0TsF7Ogn83IgtGZaaswgPefk34I4/wHPWyA==;EndpointSuffix=core.windows.net";
        static void Main(string[] args)
        {
            
            //CreateTable(cosmosdb_conn_string, "Customer");
            //CreateTable(azuretable_conn_string, "Customer");
            //InsertCustomer(cosmosdb_conn_string, 1, "John");
            //InsertCustomer(azuretable_conn_string, 1, "John");
            ReadCustomer(cosmosdb_conn_string, "1");
            ReadCustomer(azuretable_conn_string, "1");

            Console.ReadKey();
        }

        private static void ReadCustomer(string p_conn, string p_ID)
        {
            CloudStorageAccount whizlabsStorage = CloudStorageAccount.Parse(p_conn);
            CloudTableClient whizlabsTableClient = whizlabsStorage.CreateCloudTableClient();
            CloudTable whizlabsTable = whizlabsTableClient.GetTableReference("Customer");

            TableQuery<Customer> query = new TableQuery<Customer>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, p_ID));
            foreach (Customer entity in whizlabsTable.ExecuteQuery(query))
            {
                Console.WriteLine("{0}, {1}, {2}", entity.PartitionKey, entity.RowKey, entity.Email);
            }
        }

        private static void InsertCustomer(string p_conn, int p_ID, string p_Name)
        {
            CloudStorageAccount whizlabsStorage = CloudStorageAccount.Parse(p_conn);
            CloudTableClient whizlabsTableClient = whizlabsStorage.CreateCloudTableClient();
            CloudTable whizlabsTable = whizlabsTableClient.GetTableReference("Customer");

            Customer customer = new Customer(p_ID, p_Name);
            customer.Email = "john@go.com";

            TableOperation insertOperation = TableOperation.Insert(customer);

            whizlabsTable.Execute(insertOperation);

            Console.WriteLine("Entity Inserted");
        }

        private static void CreateTable(string p_conn, string p_tablename)
        {
            CloudStorageAccount whizlabsStorage = CloudStorageAccount.Parse(p_conn);
            CloudTableClient whizlabsTableClient = whizlabsStorage.CreateCloudTableClient();
            CloudTable whizlabsTable = whizlabsTableClient.GetTableReference(p_tablename);

            whizlabsTable.CreateIfNotExists();
            Console.WriteLine("Table Created");
        }
    }
}
