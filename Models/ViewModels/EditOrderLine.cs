using System.ComponentModel.DataAnnotations;

namespace SalesOrder.Models.ViewModels
{
    public class EditOrderLine
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string ProductCode { get; set; } = string.Empty;

        public ProductType ProductType { get; set; }

        [Required]
        public double ProductCostPrice { get; set; }

        [Required]
        public double ProductSalesPrice { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
