using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesOrder.Models.ViewModels
{
    public class CreateOrderHeader
    {
        [Required]
        public string OrderNumber { get; set; } = string.Empty;

        public OrderType OrderType { get; set; }

        public OrderStatus OrderStatus { get; set; }

        [Required]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        public DateTime OrderCreated { get; set; }
    }
}
