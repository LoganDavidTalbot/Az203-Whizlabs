using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchApp
{
    [SerializePropertyNamesAsCamelCase]
    public class Customer
    {
        [System.ComponentModel.DataAnnotations.Key]
        [IsFilterable]
        public string customerid { get; set; }
        [IsFilterable,IsSearchable]
        public string customername { get; set; }

        public string customeremail { get; set; }
    }
}
