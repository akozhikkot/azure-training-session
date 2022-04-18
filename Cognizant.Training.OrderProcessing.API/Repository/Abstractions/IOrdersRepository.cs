using Cognizant.Training.OrderProcessing.API.Domain;

namespace Cognizant.Training.OrderProcessing.API.Repository.Abstractions
{
    public interface IOrdersRepository
    {
        Task<string> Add(Order order, CancellationToken token = default);
        Task<Order?> Get(string id, CancellationToken token = default);
    }
}