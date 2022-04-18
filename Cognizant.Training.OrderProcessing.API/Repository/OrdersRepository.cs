using Cognizant.Training.OrderProcessing.API.Domain;
using Cognizant.Training.OrderProcessing.API.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Cognizant.Training.OrderProcessing.API.Repository
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly OrdersDbContext _context;

        public OrdersRepository(OrdersDbContext context)
        {
            _context = context;
        }

        public async Task<Order?> Get(string id, CancellationToken token = default)
        {
            return await _context.Orders.FirstOrDefaultAsync(x => x.Id == id, token);
        }

        public async Task<string> Add(Order order, CancellationToken token = default)
        {
            await _context.Orders.AddAsync(order, token);
            await _context.SaveChangesAsync(token);
            return order.Id;
        }
    }
}
