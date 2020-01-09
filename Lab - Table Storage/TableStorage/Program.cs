using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableStorage
{
    class Program
    {
        static string conn_string = "DefaultEndpointsProtocol=https;AccountName=whizlabsstore;AccountKey=LPHKW9AAxAVo5Oro3ue4EuWkfHNARLHt0YYq4etLhy/QTE+c7MK0TsF7Ogn83IgtGZaaswgPefk34I4/wHPWyA==;EndpointSuffix=core.windows.net";
        static void Main(string[] args)
        {
            //CreateTable("Customer1");
            //InsertCustomer(1, "John");
            //ReadCustomer("1");
            //ReadCustomerKeys("1", "John");
            InsertBatch();

            Console.ReadKey();
        }

        private static void InsertBatch()
        {
            CloudStorageAccount whizlabsStorage = CloudStorageAccount.Parse(conn_string);
            CloudTableClient whizlabsTableClient = whizlabsStorage.CreateCloudTableClient();
            CloudTable whizlabsTable = whizlabsTableClient.GetTableReference("Customer1");

            TableBatchOperation batch = new TableBatchOperation();

            Customer1 customer1 = new Customer1(4, "May");
            customer1.Email = "may@go.com";
            Customer1 customer2 = new Customer1(4, "Carrie");
            customer2.Email = "carrie@go.com";

            batch.Insert(customer1);
            batch.Insert(customer2);

            whizlabsTable.ExecuteBatch(batch);

            Console.WriteLine("Records Inserted");
            
        }

        private static void ReadCustomerKeys(string p_partitionKey, string p_rowKey)
        {
            CloudStorageAccount whizlabsStorage = CloudStorageAccount.Parse(conn_string);
            CloudTableClient whizlabsTableClient = whizlabsStorage.CreateCloudTableClient();
            CloudTable whizlabsTable = whizlabsTableClient.GetTableReference("Customer1");

            TableOperation retrieve = TableOperation.Retrieve<Customer1>(p_partitionKey, p_rowKey);

            TableResult result = whizlabsTable.Execute(retrieve);

            Console.WriteLine(((Customer1)result.Result).Email);
        }

        private static void ReadCustomer(string p_ID)
        {
            CloudStorageAccount whizlabsStorage = CloudStorageAccount.Parse(conn_string);
            CloudTableClient whizlabsTableClient = whizlabsStorage.CreateCloudTableClient();
            CloudTable whizlabsTable = whizlabsTableClient.GetTableReference("Customer1");

            TableQuery<Customer1> query = new TableQuery<Customer1>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, p_ID));
            foreach (Customer1 entity in whizlabsTable.ExecuteQuery(query))
            {
                Console.WriteLine("{0}, {1}, {2}",entity.PartitionKey, entity.RowKey, entity.Email);
            }
        }

        private static void InsertCustomer(int p_ID, string p_Name)
        {
            CloudStorageAccount whizlabsStorage = CloudStorageAccount.Parse(conn_string);
            CloudTableClient whizlabsTableClient = whizlabsStorage.CreateCloudTableClient();
            CloudTable whizlabsTable = whizlabsTableClient.GetTableReference("Customer1");

            Customer1 customer = new Customer1(p_ID, p_Name);
            customer.Email = "john@go.com";

            TableOperation insertOperation = TableOperation.Insert(customer);

            whizlabsTable.Execute(insertOperation);

            Console.WriteLine("Entity Inserted");
        }

        private static void CreateTable(string p_tablename)
        {
            CloudStorageAccount whizlabsStorage = CloudStorageAccount.Parse(conn_string);
            CloudTableClient whizlabsTableClient = whizlabsStorage.CreateCloudTableClient();
            CloudTable whizlabsTable = whizlabsTableClient.GetTableReference(p_tablename);

            whizlabsTable.CreateIfNotExists();
            Console.WriteLine("Table Created");
        }
    }
}
