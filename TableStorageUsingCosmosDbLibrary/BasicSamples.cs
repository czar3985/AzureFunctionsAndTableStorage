using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using TableStorageUsingCosmosDbLibrary.Model;

namespace TableStorageUsingCosmosDbLibrary
{
    public class BasicSamples
    {
        public async Task RunSamples(ILogger log, ExecutionContext context)
        {
            log.LogInformation("Azure Cosmos DB Table - Basic Samples\n");
            log.LogInformation("");

            string tableName = "demoTable";

            // Create or reference an existing table
            CloudTable table = await Common.CreateTableAsync(tableName, log, context);

            try
            {
                // Demonstrate basic CRUD functionality 
                await BasicDataOperationsAsync(table, log);
            }
            finally
            {
                // Delete the table
                //await table.DeleteIfExistsAsync();
            }
        }

        private static async Task BasicDataOperationsAsync(CloudTable table, ILogger log)
        {
            // Create an instance of a customer entity. See the Model\Customer.cs for a description of the entity.
            Customer customer = new Customer("Harp", "Walter")
            {
                Email = "Walter@contoso.com",
                PhoneNumber = "425-555-0101"
            };

            // Demonstrate how to insert the entity
            log.LogInformation("Insert an Entity.");
            customer = await TableOperations.InsertOrMergeEntityAsync(table, customer, log);

            // Demonstrate how to Update the entity by changing the phone number
            log.LogInformation("Update an existing Entity using the InsertOrMerge Upsert Operation.");
            customer.PhoneNumber = "425-555-0105";
            await TableOperations.InsertOrMergeEntityAsync(table, customer, log);
            log.LogInformation("");

            // Demonstrate how to Read the updated entity using a point query 
            log.LogInformation("Reading the updated Entity.");
            customer = await TableOperations.RetrieveEntityUsingPointQueryAsync(table, "Harp", "Walter", log);
            log.LogInformation("");

            // Demonstrate how to Delete an entity
            //log.LogInformation("Delete the entity. ");
            //await TableOperations.DeleteEntityAsync(table, customer);
            //log.LogInformation();
        }
    }
}
