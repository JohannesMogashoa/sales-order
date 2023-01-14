using Newtonsoft.Json;

namespace SalesOrder.Models.RestModels
{
    /// <summary>
    /// Rest Model : Error
    /// </summary>
    public class ErrorRestModel
    {
        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("errors")]
        public List<string> Errors { get; set; }
    }
}
