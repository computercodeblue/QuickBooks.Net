using Newtonsoft.Json;
using QuickBooks.Net.Payments.Data.Models.Fields;

namespace QuickBooks.Net.Payments.Data.Models
{
    public class CaptureCharge
    {
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("context")]
        public PaymentContext Context { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
