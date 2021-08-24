using Microsoft.Azure.Cosmos.Table;

namespace TableStorageUsingCosmosDbLibrary.Model
{
    public class Customer : TableEntity
    {
        public Customer()
        {
            
        }

        public Customer(string lastName, string firstName)
        {
            PartitionKey = lastName;
            RowKey = firstName;
        }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
