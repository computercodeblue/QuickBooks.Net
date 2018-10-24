using Newtonsoft.Json;

namespace QuickBooks.Net.Payments.Data.Models
{
    public class DeviceInfo : QuickBooksPaymentsBaseModelString
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("encrypted")]
        public bool Encrypted { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("macAddress")]
        public string MacAddress { get; set; }

        [JsonProperty("ipAddress")]
        public string IpAddress { get; set; }

        internal override QuickBooksPaymentsBaseModelString CreateReturnObject()
        {
            return this;
        }

        internal override QuickBooksPaymentsBaseModelString UpdateReturnObject()
        {
            return this;
        }

        internal override QuickBooksPaymentsBaseModelString DeleteReturnObject()
        {
            return this;
        }
    }
}
