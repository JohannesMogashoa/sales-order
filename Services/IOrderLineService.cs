using SalesOrder.Models;
using SalesOrder.Models.ViewModels;

namespace SalesOrder.Services
{
    public interface IOrderLineService
    {
        Task<string> CreateOrderLineAsync(string id, AddOrderLine orderLine);
        Task<string> UpdateOrderLineAsync(EditOrderLine orderLine);
        Task<string> DeleteOrderLineAsync(string id);
    }
}
