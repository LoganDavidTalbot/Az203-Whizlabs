using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisCacheApp
{
    public class Customer
    {
        public Customer(int id, string Name, string email)
        {
            Id = id;
            this.Name = Name;
            Email = email;
        }

        public int Id { get; }
        public string Name { get; }
        public string Email { get; }
    }
}
