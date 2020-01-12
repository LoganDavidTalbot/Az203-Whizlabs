using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASApp
{
    class Program
    {
        static void Main(string[] args)
        {
            GetBLOBProperties("output", "Sameple.txt").GetAwaiter().GetResult();
            Console.ReadKey();
        }

        private static string GetAccountSASToken()
        {
            const string connectionString = "DefaultEndpointsProtocol=https;AccountName=whizlabsstore;AccountKey=8CjlThv9HZxU5lgKSaYDrTuudMHz1YUk55beoKFYiaSoW2FJO/0UPtxfIwrhaf6BRyT6I7RQXIUVcHmTzheXdg==;EndpointSuffix=core.windows.net";
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(connectionString);

            SharedAccessAccountPolicy policy = new SharedAccessAccountPolicy()
            {
                Permissions = SharedAccessAccountPermissions.Read | SharedAccessAccountPermissions.List,
                Services = SharedAccessAccountServices.Blob,
                ResourceTypes = SharedAccessAccountResourceTypes.Service | SharedAccessAccountResourceTypes.Container,
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(24),
                Protocols = SharedAccessProtocol.HttpsOnly
            };

            return cloudStorageAccount.GetSharedAccessSignature(policy);
        }

        private static async Task GetBLOBProperties(string containerName, string blobName)
        {
            StorageCredentials storageCredentials = new StorageCredentials(GetAccountSASToken());
            CloudStorageAccount cloudStorageAccount = new CloudStorageAccount(storageCredentials, "whizlabsstore", endpointSuffix: null, useHttps: true);
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);

            BlobContinuationToken token = null;

            do
            {
                var result = await cloudBlobContainer.ListBlobsSegmentedAsync(null, token);

                token = result.ContinuationToken;

                foreach (var item in result.Results)
                {
                    Console.WriteLine($"{item.Uri}, {item.StorageUri}");
                }
            } while (token != null);
        }
    }
}
