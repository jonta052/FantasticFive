using Azure.Storage.Queues;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using QueueClient = Azure.Storage.Queues.QueueClient;

namespace SubscriptionActions
{
    internal class Queue
    {
        public static async Task<IActionResult> SendToQueue(List<string> sendsToQueue)
        {
            //Send maildata to Queue-Trigger for each subscriber
            foreach (var sendToQueue in sendsToQueue)
            {
                string requestBody = sendToQueue;

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
                        await queueClient.SendMessageAsync(requestBody);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }

                string responseMessage = string.IsNullOrEmpty("whatever")
                    ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                    : $"Hello, \"whatever\". This HTTP triggered function executed successfully.";

                
            }
            return new OkObjectResult("hejhej");
        }
    }
}
