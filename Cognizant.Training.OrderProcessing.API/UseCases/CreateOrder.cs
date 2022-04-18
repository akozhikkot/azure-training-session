using AutoMapper;
using Cognizant.Training.OrderProcessing.API.Domain;
using Cognizant.Training.OrderProcessing.API.Dto;
using Cognizant.Training.OrderProcessing.API.Infrastructure;
using Cognizant.Training.OrderProcessing.API.Message;
using Cognizant.Training.OrderProcessing.API.Repository.Abstractions;
using Cognizant.Training.OrderProcessing.API.UseCases.Abstractions;

namespace Cognizant.Training.OrderProcessing.API.UseCases
{
    public class CreateOrder : ICreateOrder
    {
        private readonly IOrdersRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMessageSender _messageSender;
        
        public CreateOrder(IOrdersRepository repository, IMapper mapper, IMessageSender messageSender)
        {
            _repository = repository;
            _mapper = mapper;
            _messageSender = messageSender;
        }

        public async Task<string> Execute(CreateOrderDto request, CancellationToken token = default)
        {
            var order = _mapper.Map<Order>(request);
            var orderId = await _repository.Add(order, token);
            await _messageSender.Send<OrderCreatedMessage>(new OrderCreatedMessage(orderId));
            return orderId;
        }
    }
}
