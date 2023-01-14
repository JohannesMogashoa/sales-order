using SalesOrder.Models;
using SalesOrder.Models.ViewModels;

namespace SalesOrder.Helpers
{
    public static class ModelHelpers
    {
        public static EditOrderHeader GetEditOrderHeader(OrderHeader order)
        {
            var editOrderModel = new EditOrderHeader
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                OrderStatus = order.OrderStatus,
                OrderType = order.OrderType,
                CustomerName = order.CustomerName,
                OrderCreated = order.OrderCreated
            };

            return editOrderModel;
        }

        public static EditOrderLine GetEditOrderLine(OrderLine orderLine)
        {
            var editOrderLineModel = new EditOrderLine
            {
                Id = orderLine.Id,
                ProductCode = orderLine.ProductCode,
                ProductType = orderLine.ProductType,
                ProductCostPrice = orderLine.ProductCostPrice,
                ProductSalesPrice = orderLine.ProductSalesPrice,
                Quantity = orderLine.Quantity,
            };

            return editOrderLineModel;
        }
    }
}
