using Azure;
using Azure.Data.Tables;
using Cognizant.Training.StorageAccount.API.Model;

namespace Cognizant.Training.StorageAccount.API.Repository
{
    public class CustomerTableStorageRepository
    {
        private TableClient tableClient;

        public CustomerTableStorageRepository(TableClient tableClient)
        {
            this.tableClient = tableClient;
        }

        public List<Customer> GetAll(string partitionKey)
        {            
            var customers = new List<Customer>();

            Pageable<TableEntity> queryResultsFilter =
                tableClient.Query<TableEntity>(filter: $"PartitionKey eq '{partitionKey}'");

            foreach (TableEntity entity in queryResultsFilter)
            {
                customers.Add(new Customer
                {
                    RowKey = entity.RowKey,
                    PartitionKey = entity.PartitionKey,
                    FirstName = entity.GetString("FirstName"),
                    LastName = entity.GetString("LastName"),
                    Email = entity.GetString("Email"),
                    Telephone = entity.GetString("Telephone")
                });
            }

            return customers;
        }

        public void AddCustomer(Customer customer)
        {
            var entity = new TableEntity(customer.PartitionKey, customer.RowKey)
            {
                { "FirstName", customer.FirstName },
                { "LastName", customer.LastName },
                { "Email", customer.Email },
                { "Telephone", customer.Telephone }
            };

            tableClient.AddEntity(entity);
        }
    }
}
