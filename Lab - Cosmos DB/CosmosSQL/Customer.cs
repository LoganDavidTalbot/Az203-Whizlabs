using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosSQL
{
    public class Customer
    {
        [JsonProperty(PropertyName = "id")]
        public string CustomerId { get; set; }
        [JsonProperty(PropertyName = "customerName")]
        public string CustomerName { get; set; }
        [JsonProperty(PropertyName = "customerEmail")]
        public string CustomerEmail { get; set; }

        public Customer(string customerId, string customerName, string customerEmail)
        {
            CustomerId = customerId;
            CustomerName = customerName;
            CustomerEmail = customerEmail;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
