using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace MSIApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ReadBLOB("https://whizlabsstore.blob.core.windows.net/demo/Sample.txt");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            

            Console.ReadKey();
        }

        private static void ReadBLOB(string uri)
        {
            string accessToken = GetAccessToken();
            Console.WriteLine(accessToken);

            TokenCredential tokenCredential = new TokenCredential(accessToken);
            StorageCredentials storageCredentials = new StorageCredentials(tokenCredential);

            Uri blobAddress = new Uri(uri);

            CloudBlockBlob blob = new CloudBlockBlob(blobAddress, storageCredentials);

            Console.WriteLine(blob.DownloadText());
            Console.WriteLine();
            Console.WriteLine("Read Complete");
        }

        private static string GetAccessToken()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://169.254.169.254/metadata/identity/oauth2/token?api-version=2018-02-01&resource=https://management.azure.com/");
            request.Headers["Metadata"] = "true";
            request.Method = "GET";

            try
            {
                // Call /token endpoint
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                // Pipe response Stream to a StreamReader, and extract access token
                StreamReader streamResponse = new StreamReader(response.GetResponseStream());
                string stringResponse = streamResponse.ReadToEnd();
                Console.WriteLine(stringResponse);
                Auth a = JsonConvert.DeserializeObject<Auth>(stringResponse);
                string accessToken = a.access_token;
                return accessToken;
            }
            catch (Exception e)
            {
                string errorText = String.Format("{0} \n\n{1}", e.Message, e.InnerException != null ? e.InnerException.Message : "Acquire token failed");
                Console.WriteLine(errorText);
            }
            return null;
           
        }
    }

    public class Auth
    {
        public string access_token { get; set; }
    }
}
