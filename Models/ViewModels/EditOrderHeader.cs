using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesOrder.Models.ViewModels
{
    public class EditOrderHeader
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string OrderNumber { get; set; }

        public OrderType OrderType { get; set; }

        public OrderStatus OrderStatus { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public DateTime OrderCreated { get; set; }
    }
}
