using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHubApp
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/azure/event-hubs/event-hubs-dotnet-framework-getstarted-send
    /// </summary>
    class Program
    {
        private const string eventHubConnectionString = "Endpoint=sb://whizlabslogan.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=L97xJlmdunLkZ3zUAdyjNil1flEpblcWWHcauV2mqek=";
        private const string eventHubName = "whizlabshub";
        private const string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=storage20201111;AccountKey=oXHTDOl5O1jKsEkqxpy12CGhKzxCM9ytRlxgTMbfB4UiKtEnx+8xb9cw+8VSV3TuFXNjW6ZExOTh986N8J+xvQ==;EndpointSuffix=core.windows.net";
        static void Main(string[] args)
        {
            //SentMessage();
            ReadMessage();

            Console.ReadKey();
        }

        private static void ReadMessage()
        {
            string eventProcessorHostName = Guid.NewGuid().ToString();
            EventProcessorHost eventProcessorHost = new EventProcessorHost(eventProcessorHostName, eventHubName, EventHubConsumerGroup.DefaultGroupName, eventHubConnectionString, storageConnectionString);
            var options = new EventProcessorOptions();
            options.ExceptionReceived += (sender, e) => { Console.WriteLine(e.Exception); };
            eventProcessorHost.RegisterEventProcessorAsync<SimpleEventProcessor>(options).Wait();
            Console.ReadLine();
            eventProcessorHost.UnregisterEventProcessorAsync().Wait();
        }

        private static void SentMessage()
        {
            var hubclient = EventHubClient.CreateFromConnectionString(eventHubConnectionString, eventHubName);
            var message = "";

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Sending Message {i}");
                message = "Message " + i;
                hubclient.Send(new EventData(Encoding.UTF8.GetBytes(message)));
            }
        }
    }
}
