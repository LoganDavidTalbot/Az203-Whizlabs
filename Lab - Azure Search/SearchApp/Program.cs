using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchApp
{
    class Program
    {
        private static string searchServiceName = "whizlabsearch";
        private static string adminApiKey = "53080C7D9BBEFD30BB1D40EC6690EC86";
        static void Main(string[] args)
        {
            //CreateIndex();
            //UploadData();
            SearchIndex();

            Console.ReadKey();
        }

        private static void SearchIndex()
        {
            SearchServiceClient serviceClient = new SearchServiceClient(searchServiceName, new SearchCredentials(adminApiKey));
            ISearchIndexClient indexClient = serviceClient.Indexes.GetClient("customer");

            SearchParameters parameters;
            DocumentSearchResult<Customer> results;

            parameters = new SearchParameters()
            {
                Select = new[] { "customername","customeremail" }
            };

            results = indexClient.Documents.Search<Customer>("Mark", parameters);

            foreach (SearchResult<Customer> result in results.Results)
            {
                Console.WriteLine($"{result.Document.customername},{result.Document.customeremail}");
            }
        }

        private static void UploadData()
        {
            var actions = new IndexAction<Customer>[]
            {
                IndexAction.Upload(new Customer(){customerid="1", customername="John",customeremail="john@go.com"}),
                IndexAction.Upload(new Customer(){customerid="2", customername="Mark",customeremail="mark@go.com"})
            };

            var batch = IndexBatch.New(actions);

            SearchServiceClient serviceClient = new SearchServiceClient(searchServiceName, new SearchCredentials(adminApiKey));

            ISearchIndexClient indexClient = serviceClient.Indexes.GetClient("customer");
            indexClient.Documents.Index(batch);

            Console.WriteLine("Added Data");

            Console.ReadKey();
        }

        private static void CreateIndex()
        {
            var definition = new Index()
            {
                Name = "customer",
                Fields = FieldBuilder.BuildForType<Customer>()
            };

            SearchServiceClient serviceClient = new SearchServiceClient(searchServiceName, new SearchCredentials(adminApiKey));
            serviceClient.Indexes.Create(definition);
            Console.WriteLine("Search Index Created");
        }
    }
}
