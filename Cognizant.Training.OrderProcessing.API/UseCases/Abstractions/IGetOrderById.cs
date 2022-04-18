using Cognizant.Training.OrderProcessing.API.Dto;

namespace Cognizant.Training.OrderProcessing.API.UseCases.Abstractions
{
    public interface IGetOrderById
    {
        Task<OrderDto?> Execute(string id, CancellationToken token = default);
    }
}