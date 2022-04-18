using Cognizant.Training.OrderProcessing.API.Dto;

namespace Cognizant.Training.OrderProcessing.API.UseCases.Abstractions
{
    public interface ICreateOrder
    {
        Task<string> Execute(CreateOrderDto request, CancellationToken token = default);
    }
}