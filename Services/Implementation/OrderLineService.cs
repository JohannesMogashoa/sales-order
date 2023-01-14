using Microsoft.EntityFrameworkCore;
using SalesOrder.Controllers;
using SalesOrder.Data;
using SalesOrder.Models;
using SalesOrder.Models.ViewModels;

namespace SalesOrder.Services.Implementation
{
    public class OrderLineService : IOrderLineService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<OrderHeadersController> _logger;

        public OrderLineService(ApplicationDbContext context, ILogger<OrderHeadersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<string> CreateOrderLineAsync(string id, AddOrderLine orderLine)
        {
            OrderHeader orderHeader = _context.OrderHeaders.Where(oH => oH.Id == id).Include(oH => oH.OrderLines).FirstOrDefault();
            if (orderHeader != null && orderHeader.Archived == false)
            {
                int totalLines = orderHeader.OrderLines.Count;
                var lineNumber = GetOrderLineNumber(totalLines);

                var newOrderLine = new OrderLine
                {
                    ProductCode = orderLine.ProductCode,
                    ProductType = orderLine.ProductType,
                    ProductCostPrice = orderLine.ProductCostPrice,
                    ProductSalesPrice = orderLine.ProductSalesPrice,
                    Quantity = orderLine.Quantity,
                    LineNumber = lineNumber,
                    OrderHeader = orderHeader
                };

                _context.Add(newOrderLine);
                await _context.SaveChangesAsync();
                _logger.LogInformation("New orderline created");

                return $"/Home/Order/{id}";
            }
            return "/Home/Orders";
        }

        public async Task<string> DeleteOrderLineAsync(string id)
        {
            OrderLine orderLine = _context.OrderLines.Where(x => x.Id == id).Include(x => x.OrderHeader).FirstOrDefault();
            orderLine.Archived = true;

            _context.Update(orderLine);
            await _context.SaveChangesAsync();
            _logger.LogInformation($">>>>> Order Line, id: {orderLine.Id}, deleted!");

            return $"/Home/Order/{orderLine.OrderHeader.Id}";
        }

        public async Task<string> UpdateOrderLineAsync(EditOrderLine orderLine)
        {
            OrderLine _orderLine = _context.OrderLines.Where(x => x.Id == orderLine.Id).Include(x => x.OrderHeader).FirstOrDefault();
            _orderLine.ProductCode = orderLine.ProductCode;
            _orderLine.ProductType = orderLine.ProductType;
            _orderLine.ProductCostPrice = orderLine.ProductCostPrice;
            _orderLine.ProductSalesPrice = orderLine.ProductSalesPrice;
            _orderLine.Quantity = orderLine.Quantity;

            _context.Update(_orderLine);

            await _context.SaveChangesAsync();
            _logger.LogInformation($">>>>> Order Line, id:  {_orderLine.Id}, updated! <<<<<");

            return $"/Home/Order/{_orderLine.OrderHeader.Id}";
        }

        private static int GetOrderLineNumber(int currentLines)
        {
            int lineNumber = 1;

            if (currentLines > 0)
            {
                lineNumber = currentLines + 1;

                return lineNumber;
            }

            return lineNumber;
        }
    }
}
