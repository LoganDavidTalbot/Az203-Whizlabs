using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisCacheApp
{
    class Program
    {
        private static string conn_string = "whizlabsr.redis.cache.windows.net:6380,password=RUGxZYphg+J9dMkzZ6W2dCW5aNX7o5IXJbcBwJz2aOw=,ssl=True,abortConnect=False";
        static void Main(string[] args)
        {
            IDatabase cache = lazyConnection.Value.GetDatabase();
            //Console.WriteLine(cache.StringSet("KeyA", "ValueA").ToString());
            //Console.WriteLine(cache.StringGet("KeyA").ToString());

            // Get All keys

            //var endpoints = cache.Multiplexer.GetEndPoints();
            //var server = cache.Multiplexer.GetServer(endpoints.First());
            //var keys = server.Keys(cache.Database);

            //foreach (var key in keys)
            //{
            //    Console.WriteLine(key);
            //}

            // to delete a key
            //cache.KeyDelete("KeyA");

            // Adding Class object
            Customer obj = new Customer(1, "John", "john@go0.com");
            cache.StringSet("CustomerA", JsonConvert.SerializeObject(obj));

            Customer cache_obj = JsonConvert.DeserializeObject<Customer>(cache.StringGet("CustomerA"));

            Console.WriteLine($"{cache_obj.Id}, {cache_obj.Name}, {cache_obj.Email}");

            Console.ReadKey();
        }

        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect(conn_string);
        });

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }
}
