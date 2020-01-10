using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosTableApi
{
    public class Customer : TableEntity
    {
        public Customer(int id, string name)
        {
            this.PartitionKey = id.ToString();
            this.RowKey = name;
        }
        public Customer()
        {

        }

        public string Email { get; set; }
    }

}
