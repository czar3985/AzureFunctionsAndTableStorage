using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace TableStorage
{
    public static class TableStorage
    {
        public class MyPoco
        {
            public string PartitionKey { get; set; }
            public string RowKey { get; set; }
            public string Text { get; set; }
        }

        [FunctionName("TableOutput")]
        [return: Microsoft.Azure.WebJobs.Table("MyTable")]
        public static MyPoco Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            // Trigger: http://localhost:7071/api/TableOutput?text=hello
            // Creates a table MyTable if it doesn't exist and adds a row to it. Timestamp added automatically 
            string text = req.Query["text"];
            log.LogInformation($"C# http trigger function processed: {text}");
            return new MyPoco { PartitionKey = "Http", RowKey = Guid.NewGuid().ToString(), Text = text };
        }
    }
}
