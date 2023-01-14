using Microsoft.EntityFrameworkCore;
using SalesOrder.Controllers;
using SalesOrder.Data;
using SalesOrder.Models;
using SalesOrder.Models.ViewModels;

namespace SalesOrder.Services.Implementation
{
    public class OrderHeaderService : IOrderHeaderService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<OrderHeadersController> _logger;

        public OrderHeaderService(ApplicationDbContext context, ILogger<OrderHeadersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<OrderHeader> CreateOrderHeaderAsync(CreateOrderHeader order)
        {
            var newOrder = new OrderHeader
            {
                OrderNumber = order.OrderNumber,
                OrderType = order.OrderType,
                OrderStatus = order.OrderStatus,
                CustomerName = order.CustomerName,
                OrderCreated = order.OrderCreated,
            };

            _context.Add(newOrder);
            await _context.SaveChangesAsync();
            _logger.LogInformation($">>>>> Order Header, id: {newOrder.Id}, created! <<<<<");

            return newOrder;
        }

        public async Task<Task> DeleteOrderHeaderAsync(string id)
        {
            var order = _context.OrderHeaders.FirstOrDefault(x => x.Id == id);
            order.Archived = true;

            _context.Update(order);
            await _context.SaveChangesAsync();

            _logger.LogInformation($">>>>> Order Header, id: {order.Id}, deleted! <<<<<");

            return Task.CompletedTask;
        }

        public async Task<Task> EditOrderHeaderAsync(EditOrderHeader order)
        {
            var _order = _context.OrderHeaders.FirstOrDefault(x => x.Id == order.Id);

            _order.OrderNumber = order.OrderNumber;
            _order.OrderStatus = order.OrderStatus;
            _order.CustomerName = order.CustomerName;
            _order.OrderType = order.OrderType;
            _order.OrderCreated = order.OrderCreated;

            _context.Update(_order);

            await _context.SaveChangesAsync();
            _logger.LogInformation($">>>>> Order Header, id: {_order.Id}, updated! <<<<<");

            return Task.CompletedTask;
        }

        public OrderHeader GetOrderHeader(string id)
        {
            var orderHeader = _context.OrderHeaders
                .Where(oH => oH.Id.Equals(id) && oH.Archived == false)
                .Include(oH => oH.OrderLines.Where(oL => oL.Archived == false).OrderBy(oL => oL.LineNumber))
                .FirstOrDefault();

            return orderHeader;
        }

        public List<OrderHeader> GetOrderHeaders()
        {
            var orderHeaders = _context.OrderHeaders.Where(oH => oH.Archived == false).ToList();
            return orderHeaders;
        }
    }
}
