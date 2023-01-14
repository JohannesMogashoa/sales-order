using SalesOrder.Models;
using SalesOrder.Models.ViewModels;

namespace SalesOrder.Services
{
    public interface IOrderHeaderService
    {
        List<OrderHeader> GetOrderHeaders();
        OrderHeader GetOrderHeader(string id);
        Task<OrderHeader> CreateOrderHeaderAsync(CreateOrderHeader order);
        Task<Task> EditOrderHeaderAsync(EditOrderHeader order);
        Task<Task> DeleteOrderHeaderAsync(string id);
    }
}
