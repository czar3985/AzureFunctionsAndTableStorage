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
                newClient.ResponseMessage =
                    $"CreateClientHandler HTTP triggered function executed successfully for {name}.";
            }
            else
            {
                newClient.ResponseMessage =
                    "CreateClientHandler HTTP triggered function executed successfully. Pass a client name in the query string to create a new client.";
            }

            return new OkObjectResult(newClient);
        }
    }
}
