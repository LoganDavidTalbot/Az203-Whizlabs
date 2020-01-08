//#r "Newtonsoft.Json"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

public static async Task<IActionResult> Run(HttpRequest req, ILogger log)
{
    log.LogInformation("This is sampel function");

    string p_ID = req.Query["ID"];
    return(ActionResult)new OkObjectResult($"Parameter you have passed, {p_ID}");
}