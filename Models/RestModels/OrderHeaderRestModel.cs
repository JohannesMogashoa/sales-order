using Newtonsoft.Json;

namespace SalesOrder.Models.RestModels
{
    /// <summary>
    /// Rest Model : Order Header
    /// </summary>
    public class OrderHeaderRestModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("order_number")]
        public string OrderNumber { get; set; }

        [JsonProperty("order_type")]
        public OrderType OrderType { get; set; }

        [JsonProperty("order_status")]
        public OrderStatus OrderStatus { get; set; }

        [JsonProperty("customer_name")]
        public string CustomerName { get; set; }

        [JsonProperty("order_created")]
        public DateTime OrderCreated { get; set; }

        [JsonProperty("order_lines")]
        public List<OrderLineRestModel> OrderLines { get; set; }
    }
}
