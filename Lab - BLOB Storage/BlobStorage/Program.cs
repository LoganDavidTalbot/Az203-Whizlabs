using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobStorage
{
    class Program
    {
        private static string conn_string = "";
        static void Main(string[] args)
        {
            //CreateContainer("whizlabsdemonew").GetAwaiter().GetResult();
            //UploadBLOB("whizlabsdemonew", "Sample.txt", @"D:\Sample.txt").GetAwaiter().GetResult();
            //GetBlobProperties("whizlabsdemonew", "Sample.txt").GetAwaiter().GetResult();
            //ShowETag("whizlabsdemonew", "Sample.txt").GetAwaiter().GetResult();
            //GetLease("whizlabsdemonew", "Sample.txt").GetAwaiter().GetResult();
            //GetLease2("whizlabsdemonew", "Sample.txt").GetAwaiter().GetResult();
            Metadata("whizlabsdemonew", "Sample.txt").GetAwaiter().GetResult();

            Console.ReadKey();
        }

        private static async Task CreateContainer(string v)
        {
            CloudStorageAccount storage = CloudStorageAccount.Parse(conn_string);
            CloudBlobClient cloudBlobClient = storage.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(v);
            await cloudBlobContainer.CreateAsync();

            BlobContainerPermissions permissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            };
            await cloudBlobContainer.SetPermissionsAsync(permissions);
            Console.WriteLine("Created Container");
        }

        private static async Task UploadBLOB(string containerName, string filename, string location)
        {
            CloudStorageAccount storage = CloudStorageAccount.Parse(conn_string);
            CloudBlobClient cloudBlobClient = storage.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);

            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(filename);
            await cloudBlockBlob.UploadFromFileAsync(location);
            Console.WriteLine("Object Uploaded");
        }

        private static async Task GetBlobProperties(string containerName, string filename)
        {
            CloudStorageAccount storage = CloudStorageAccount.Parse(conn_string);
            CloudBlobClient cloudBlobClient = storage.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);

            BlobContinuationToken token = null;

            do
            {
                var result = await cloudBlobContainer.ListBlobsSegmentedAsync(null, token);

                token = result.ContinuationToken;

                foreach (var item in result.Results)
                {
                    Console.WriteLine($"{item.Uri}");
                }
            } while (token != null);
        }

        private static async Task ShowETag(string containerName, string filename)
        {
            CloudStorageAccount storage = CloudStorageAccount.Parse(conn_string);
            CloudBlobClient cloudBlobClient = storage.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);


            var blob = cloudBlobContainer.GetBlockBlobReference(filename);
            await blob.FetchAttributesAsync();

            var orginalETag = blob.Properties.ETag;

            blob.UploadText($"New Content {DateTime.UtcNow}", accessCondition: AccessCondition.GenerateIfMatchCondition(orginalETag));

            try
            {
                blob.UploadText($"New Content 2 - {DateTime.UtcNow}", accessCondition: AccessCondition.GenerateIfMatchCondition(orginalETag));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            await blob.FetchAttributesAsync();

            Console.WriteLine($"{orginalETag} != {blob.Properties.ETag}");
        }

        private static async Task GetLease(string containerName, string filename)
        {
            CloudStorageAccount storage = CloudStorageAccount.Parse(conn_string);
            CloudBlobClient cloudBlobClient = storage.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);


            var blob = cloudBlobContainer.GetBlockBlobReference(filename);
            await blob.FetchAttributesAsync();

            string lease = blob.AcquireLease(TimeSpan.FromSeconds(15), null);

            try
            {
                blob.UploadText($"New Content 2");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }            

            await Task.Delay(TimeSpan.FromSeconds(16));
            blob.UploadText($"New Content {DateTime.UtcNow}");

            await blob.FetchAttributesAsync();

            Console.WriteLine("Lease Ended");
        }

        private static async Task GetLease2(string containerName, string filename)
        {
            CloudStorageAccount storage = CloudStorageAccount.Parse(conn_string);
            CloudBlobClient cloudBlobClient = storage.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);


            var blob = cloudBlobContainer.GetBlockBlobReference(filename);
            await blob.FetchAttributesAsync();

            string lease = blob.AcquireLease(TimeSpan.FromSeconds(15), null);

            try
            {
                var accessCondition = AccessCondition.GenerateLeaseCondition(lease);
                blob.UploadText($"New Content 2", accessCondition: accessCondition);
                Console.WriteLine("Blob Updated");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }            
        }

        private static async Task Metadata(string containerName, string filename)
        {
            CloudStorageAccount storage = CloudStorageAccount.Parse(conn_string);
            CloudBlobClient cloudBlobClient = storage.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);


            var blob = cloudBlobContainer.GetBlockBlobReference(filename);
            await blob.FetchAttributesAsync();

            try
            {
                blob.Metadata.Add("Department", "H2R");
                await blob.SetMetadataAsync();
                Console.WriteLine("Metadata Updated");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
