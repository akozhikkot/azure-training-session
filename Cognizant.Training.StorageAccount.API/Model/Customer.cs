using Azure.Data.Tables;

namespace Cognizant.Training.StorageAccount.API.Model
{
    public class Customer 
    {
        public string? RowKey { get; set; }

        public string? PartitionKey { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? Telephone { get; set; }

    }
}
