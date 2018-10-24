using Newtonsoft.Json;

namespace Quickbooks.Net.Payments.Data.Models
{
    public class Token
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
