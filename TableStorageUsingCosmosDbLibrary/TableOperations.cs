using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Logging;
using TableStorageUsingCosmosDbLibrary.Model;

namespace TableStorageUsingCosmosDbLibrary
{
    public class TableOperations
    {
        public static async Task<Customer> InsertOrMergeEntityAsync(CloudTable table, Customer entity, ILogger log)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            try
            {
                // Create the InsertOrReplace table operation
                TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(entity);

                // Execute the operation.
                TableResult result = await table.ExecuteAsync(insertOrMergeOperation);
                Customer insertedCustomer = result.Result as Customer;

                if (result.RequestCharge.HasValue)
                {
                    log.LogInformation("Request Charge of InsertOrMerge Operation: " + result.RequestCharge);
                }

                return insertedCustomer;
            }
            catch (StorageException e)
            {
                log.LogInformation(e.Message);
                throw;
            }
        }

        public static async Task<Customer> RetrieveEntityUsingPointQueryAsync(CloudTable table, string partitionKey, string rowKey, ILogger log)
        {
            try
            {
                TableOperation retrieveOperation = TableOperation.Retrieve<Customer>(partitionKey, rowKey);
                TableResult result = await table.ExecuteAsync(retrieveOperation);
                Customer customer = result.Result as Customer;
                if (customer != null)
                {
                    log.LogInformation("\t{0}\t{1}\t{2}\t{3}", customer.PartitionKey, customer.RowKey, customer.Email, customer.PhoneNumber);
                }

                if (result.RequestCharge.HasValue)
                {
                    log.LogInformation("Request Charge of Retrieve Operation: " + result.RequestCharge);
                }

                return customer;
            }
            catch (StorageException e)
            {
                log.LogInformation(e.Message);
                throw;
            }
        }

        public static async Task DeleteEntityAsync(CloudTable table, Customer deleteEntity, ILogger log)
        {
            try
            {
                if (deleteEntity == null)
                {
                    throw new ArgumentNullException("deleteEntity");
                }

                TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
                TableResult result = await table.ExecuteAsync(deleteOperation);

                if (result.RequestCharge.HasValue)
                {
                    log.LogInformation("Request Charge of Delete Operation: " + result.RequestCharge);
                }

            }
            catch (StorageException e)
            {
                log.LogInformation(e.Message);
                throw;
            }
        }
    }
}
