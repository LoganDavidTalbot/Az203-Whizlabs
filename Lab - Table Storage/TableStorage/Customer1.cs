using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableStorage
{
    public class Customer1 : TableEntity
    {
        public Customer1(int id, string name)
        {
            this.PartitionKey = id.ToString();
            this.RowKey = name;
        }
        public Customer1()
        {

        }

        public string Email { get; set; }
    }
}
