using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace QueueTriggerTableInput
{
    public static class TableStorage
    {
        public class MyPoco
        {
            public string PartitionKey { get; set; }
            public string RowKey { get; set; }
            public DateTimeOffset Timestamp { get; set; }
            public string OriginalName { get; set; }
        }

        [FunctionName("TableInput")]
        public static void Run(
            [QueueTrigger("outqueue", Connection = "AzureWebJobsStorage")]string input,
            [Table("AzureWebJobsHostLogscommon", "FD2", "func:2Dcreate:2Dclient-create")] MyPoco poco,
            ILogger log)
        {
            // 1st azure Function was ran that inputs a message in the queue
            // It will trigger this function
            // This function reads from a table
            var message = $"PK={poco.PartitionKey}, RK={poco.RowKey}, OriginalName={poco.OriginalName}";
            log.LogInformation(message);
        }
    }
}
