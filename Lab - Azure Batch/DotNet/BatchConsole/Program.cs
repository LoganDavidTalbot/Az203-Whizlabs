using Microsoft.Azure.Batch;
using Microsoft.Azure.Batch.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchConsole
{
    class Program
    {
        private static string BatchAccountUrl = "https://whizlabsaccount.westeurope.batch.azure.com";
        private static string BatchAccountName = "whizlabsaccount";
        private static string BatchAccountKey= "";

        private const string PoolId = "Whizlabspool";
        static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        private static async Task MainAsync()
        {
            BatchSharedKeyCredentials batchSharedKeyCredentials = new BatchSharedKeyCredentials(BatchAccountUrl, BatchAccountName, BatchAccountKey);

            using (BatchClient batchClient = BatchClient.Open(batchSharedKeyCredentials))
            {
                //await CreatePoolIfNotExistAsync(batchClient, PoolId);
                //await CreatJobAsync(batchClient, "whizlabsjob", PoolId);
                await AddTasksAsync(batchClient, "whizlabsjob");
            }
            Console.WriteLine("All done");
            Console.ReadKey();
        }

        private static async Task<CloudTask> AddTasksAsync(BatchClient batchClient, string jobId)
        {
            Console.WriteLine("Creating task");
            string taskCommandLine = String.Format("cmd /c %AZ_BATCH_APP_PACKAGE_WHIZLABSAPP%\\BatchApp.exe");
            CloudTask task = new CloudTask("Task0", taskCommandLine);

            await batchClient.JobOperations.AddTaskAsync(jobId, task);
            return task;
        }

        private static async Task CreatJobAsync(BatchClient batchClient, string jobId, string poolId)
        {
            Console.WriteLine("Create job");

            CloudJob job = batchClient.JobOperations.CreateJob();
            job.Id = jobId;
            job.PoolInformation = new PoolInformation { PoolId = PoolId };

            await job.CommitAsync();
        }

        private static async Task CreatePoolIfNotExistAsync(BatchClient batchClient, string poolId)
        {
            CloudPool pool = null;

            Console.WriteLine("Creating pool of instances");
            ImageReference imageReference = new  ImageReference(
                publisher: "MicrosoftWindowsServer",
                offer: "WindowsServer",
                sku: "2016-datacenter",
                version: "latest");

            VirtualMachineConfiguration virtualMachineConfiguration = new VirtualMachineConfiguration(
                imageReference: imageReference,
                nodeAgentSkuId: "batch.node.windows amd64");

            pool = batchClient.PoolOperations.CreatePool(
                poolId: PoolId,
                targetDedicatedComputeNodes: 1,
                targetLowPriorityComputeNodes: 0,
                virtualMachineSize: "STANDARD_A1_V2",
                virtualMachineConfiguration: virtualMachineConfiguration);

            pool.ApplicationPackageReferences = new List<ApplicationPackageReference>()
            {
                new ApplicationPackageReference
                {
                    ApplicationId = "whizlabsapp",
                    Version = "1.0"
                }
            };

            await pool.CommitAsync();
        }
    }
}
