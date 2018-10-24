using Newtonsoft.Json;

namespace QuickBooks.Net.Payments.Data.Models.Fields
{
    public class PaymentContext
    {
        [JsonProperty("tax")]
        public decimal Tax { get; set; }

        [JsonProperty("deviceInfo")]
        public DeviceInfo DeviceInfo { get; set; }

        [JsonProperty("mobile")]
        public bool Mobile { get; set; }

        [JsonProperty("isEcommerce")]
        public bool IsEcommerce { get; set; }

        [JsonProperty("recurring")]
        public bool Recurring { get; set; }
    }
}
