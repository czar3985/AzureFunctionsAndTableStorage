using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ClientMaster
{
    public static class CreateClientHandler
    {
        [FunctionName("create")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [Queue("outqueue"), StorageAccount("AzureWebJobsStorage")] ICollector<string> msg,
            ILogger log)
        {
            log.LogInformation("CreateClientHandler HTTP trigger function started processing request");

            string name = req.Query["name"];

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name ??= data?.name;

            var newClient = new Client();
            if (!string.IsNullOrEmpty(name))
            {
                newClient.Id = Guid.NewGuid();
                newClient.CreatedOn = DateTimeOffset.Now;

                // Add a message to the output collection.
                msg.Add($"Client name added: {name}");

                return new OkObjectResult(newClient);
            }

            return new BadRequestObjectResult("Please pass a client name on the query string or in the request body");
        }
    }
}
