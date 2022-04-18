using AutoMapper;
using Cognizant.Training.OrderProcessing.API.Dto;
using Cognizant.Training.OrderProcessing.API.Repository.Abstractions;
using Cognizant.Training.OrderProcessing.API.UseCases.Abstractions;

namespace Cognizant.Training.OrderProcessing.API.UseCases
{
    public class GetOrderById : IGetOrderById
    {
        private readonly IOrdersRepository _repository;
        private readonly IMapper _mapper;

        public GetOrderById(IOrdersRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OrderDto?> Execute(string id, CancellationToken token = default)
        {
            var order = await _repository.Get(id, token);
            return order == null ? null : _mapper.Map<OrderDto>(order);
        }
    }
}
