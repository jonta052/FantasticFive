using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;

namespace ConfirmationEMail
{
    public static class ConfirmHttp
    {
        [FunctionName("ConfirmHttp")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //name = name ?? data?.name;

            var configuration = new ConfigurationBuilder().
                SetBasePath(Directory.GetCurrentDirectory()).
                AddJsonFile("local.settings.json", true, true).
                AddEnvironmentVariables().
                Build();

            string connectionstring = configuration["AzureWebJobsStorage"];
            string queuestring = configuration["AzureQueueName"];

            QueueClient queueClient = new QueueClient(connectionstring, queuestring, new QueueClientOptions
            { MessageEncoding = QueueMessageEncoding.Base64 });

            queueClient.CreateIfNotExists();
            if (queueClient.Exists())
            {
                try
                {
                    queueClient.SendMessage(requestBody);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}
