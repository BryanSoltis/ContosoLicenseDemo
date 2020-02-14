using System;
using System.IO;
using System.Text;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ContosoLicenseFunctions
{
    public static class ProcessUploadsFunctions
    {
        [FunctionName("ProcessUploadsFunctions")]
        public static async void Run([BlobTrigger("uploads/{name}", Connection = "StorageAccountConnection")]Stream myBlob, string name, ILogger log)
        {
            // Process the image using Cognitive Services Computer Vision!
            string subscriptionKey = "[Cognitive Vision API Key]";
            string endpoint = "[Cognitive Vision Endpoint]";

            bool blnSuccess = false;


            // Create a client
            ComputerVisionClient client = Authenticate(endpoint, subscriptionKey);
            try
            {
                OcrResult result = await client.RecognizePrintedTextAsync(true, "[Azure Storage Account Root URL]" + name, OcrLanguages.En);

                if (result != null)
                {
                    // Get the image text
                    // THIS WILL BE REPLACED BY 3rd PARTY OCR
                    StringBuilder sb = new StringBuilder();
                    foreach (OcrRegion region in result.Regions)
                    {
                        foreach (OcrLine line in region.Lines)
                        {
                            foreach (OcrWord word in line.Words)
                            {
                                sb.Append(word.Text + " ");
                            };
                        };
                    };

                    // Mkae sure the plate was recognized.
                    if (sb.ToString() != "")
                    {

                        var arr1 = new string[] { "Illinois Ave", "Marvin Gardens", "Park Place", "Baltic Ave" };
                        Random random = new Random();

                        /// Create a new CosmosDB document 
                        LicenseRecord item = new LicenseRecord()
                        {
                            Id = Guid.NewGuid().ToString(),
                            PlateNumber = sb.ToString(), // Extracted from OCR
                            Location = arr1[random.Next(arr1.Length)], // Extracted from OCR
                            PhotoURL = "[Azure Storage Account Root URL] + name, // Uploaded Photo
                            DateCaptured = DateTime.Now, // Extracted from OCR / Upload time
                            IsProcessed = false
                        };

                        using (CosmosClient cosmosdbclient = new CosmosClient("AccountEndpoint=[Azure CosmosDB endpoint];AccountKey=[Azure CosmosDB account key]"))
                        {
                            Container container = cosmosdbclient.GetContainer("[Azure CosmosDB database name]", "[Azure CosmosDB container name]");
                            if (container != null)
                            {
                                ItemResponse<[Azure CosmosDB document class]> response = await container.CreateItemAsync(item, new PartitionKey(item.Id));
                                blnSuccess = true;
                            }
                        }
                    }


                    // Move the original blob
                    // If successful, move to Processed container
                    // If failure, move to Unprocessed container
                    var cred = new StorageCredentials("[Azure Stoage Account name]", "[Azure Storage account key]");
                    var account = new CloudStorageAccount(cred, true);
                    var storageclient = account.CreateCloudBlobClient();
                    var sourceContainer = storageclient.GetContainerReference("uploads");
                    var sourceBlob = sourceContainer.GetBlockBlobReference(name);
                    string strDestinationContainer = "processed";
                    if (!blnSuccess)
                    {
                        strDestinationContainer = "unprocessed";
                    }
                    var destinationContainer = storageclient.GetContainerReference(strDestinationContainer);
                    var destinationBlob = destinationContainer.GetBlockBlobReference(name);
                    await destinationBlob.StartCopyAsync(sourceBlob);
                    await sourceBlob.DeleteIfExistsAsync();
                };

            }
            catch (Exception ex)
            {
                log.LogInformation(ex.Message);
            };
        }

        /*
 * AUTHENTICATE
 * Creates a Computer Vision client used by each example.
 */
        public static ComputerVisionClient Authenticate(string endpoint, string key)
        {
            ComputerVisionClient client =
              new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
              { Endpoint = endpoint };
            return client;
        }
    }
}

