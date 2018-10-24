using Newtonsoft.Json;

namespace QuickBooks.Net.Payments.Data.Models
{
    public class Token
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
