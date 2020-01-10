using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadData();
        }

        private static void ReadData()
        {
            //using (var db = new whizlabsdbepEntities())
            //{
            //    var entries = (from c in db.People select).ToList();
            //    
            //    Output Entities
            //}
        }

        private static void AddData()
        {
            //using (var db = new whizlabsdbepEntities())
            //{
            //    db.People.Add(new Person
            //    {
            //       ID=3,Name="Mary",Email="mary@go.com"
            //    };
            //    db.SaveChanges();
            //    Output Entities
            //}
        }
    }
}
