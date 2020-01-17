using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageQueue
{
    class Program
    {
        private static string connectionString = "";
        static void Main(string[] args)
        {
            //CreateQueue();
            //InsertQueue("myqueue","Hello, World");
            DequeueMessage("myqueue");
            Console.ReadLine();
        }

        private static void DequeueMessage(string queueName)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            // Create the queue client.
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a queue.
            CloudQueue queue = queueClient.GetQueueReference(queueName);

            CloudQueueMessage peekedMessage = queue.PeekMessage();
            Console.WriteLine(peekedMessage.AsString);

            Console.WriteLine("Message Added");
        }

        private static void InsertQueue(string queueName, string messagetext)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            // Create the queue client.
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a queue.
            CloudQueue queue = queueClient.GetQueueReference(queueName);

            // Create a message and add it to the queue.
            CloudQueueMessage message = new CloudQueueMessage(messagetext);
            queue.AddMessage(message);

            Console.WriteLine("Message Added");
        }

        private static void CreateQueue()
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            // Create the queue client.
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a queue.
            CloudQueue queue = queueClient.GetQueueReference("myqueue");

            // Create the queue if it doesn't already exist.
            queue.CreateIfNotExists();

            Console.WriteLine("Queue Created");
        }
    }
}
