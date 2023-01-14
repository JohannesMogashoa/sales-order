using SalesOrder.Data;
using SalesOrder.Models;
using SalesOrder.Models.ViewModels;
using SalesOrder.Services;

namespace SalesOrder.Helpers
{
    public class Seeder
    {
        private readonly WebApplication _app;

        public Seeder(WebApplication app)
        {
            _app = app;
        }

        public async void Seed()
        {
            var scope = _app.Services.CreateScope();
            var _orderHeaderService = scope.ServiceProvider.GetRequiredService<IOrderHeaderService>();
            var _orderLineService = scope.ServiceProvider.GetRequiredService<IOrderLineService>();
            var _usersService = scope.ServiceProvider.GetRequiredService<IUsersService>();
            var _db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if(_db.OrderHeaders.Any() || _db.OrderLines.Any() || _db.Users.Any())
            {
                return;
            } else
            {
                NewUserModel user = new()
                {
                    Email = "admin@salesorder.com",
                    Password = "#User@123",
                    ConfirmPassword = "#User@123"
                };

                CreateOrderHeader createOrderHeader = new()
                {
                    CustomerName = "KFC",
                    OrderCreated = DateTime.Now,
                    OrderNumber = "SO625144",
                    OrderStatus = OrderStatus.New,
                    OrderType = OrderType.Normal
                };

                AddOrderLine addOrderLine = new()
                {
                    ProductCode = "GSX837420",
                    ProductCostPrice = 13.54,
                    ProductSalesPrice = 84.49,
                    ProductType = ProductType.Parts,
                    Quantity = 10
                };

                OrderHeader orderHeader = await _orderHeaderService.CreateOrderHeaderAsync(createOrderHeader);

                await _orderLineService.CreateOrderLineAsync(orderHeader.Id, addOrderLine);

                await _usersService.CreateUserAsync(user);
            }
        }
    }
}
