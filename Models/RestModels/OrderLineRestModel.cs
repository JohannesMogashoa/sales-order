using Newtonsoft.Json;

namespace SalesOrder.Models.RestModels
{
    /// <summary>
    /// Rest Model : Order Line
    /// </summary>
    public class OrderLineRestModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("line_number")]
        public int LineNumber { get; set; }

        [JsonProperty("product_code")]
        public string ProductCode { get; set; }

        [JsonProperty("product_type")]
        public ProductType ProductType { get; set; }

        [JsonProperty("product_cost_price")]
        public double ProductCostPrice { get; set; }

        [JsonProperty("product_sales_price")]
        public double ProductSalesPrice { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}
