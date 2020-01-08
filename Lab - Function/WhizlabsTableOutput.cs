//#r "Newtonsoft.Json"

using System.Net;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

public static void Run(string req, ICollector<Customer> tableBinding, ILogger log)
{
    log.LogInformation("C# HTTP trigger function processed a request.");

    for (int i = 1; i <=5; i++)
    {
        log.LogInformation($"Adding customer entity {i}");
        tableBinding.Add(
            new Customer() {
                PartitionKey = i.ToString(),
                RowKey = "User" + i.ToString(),
                Email = "User" + i.ToString() + "@whizlabs.com",
            }
        );
    }
}

public class Customer
{
    public string PartitionKey {get; set;}
    public string RowKey {get; set;}
    public string Email {get; set;}
}
