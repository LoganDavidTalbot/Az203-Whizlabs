using System.Net;

public static async Task<HttpResponseMessage> Run(HttpResponseMessage req, ILogger log)
{
	log.LogInformation("Hello World");
	return null;
}