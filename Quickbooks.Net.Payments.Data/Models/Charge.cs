using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using QuickBooks.Net.Payments.Data.Models.Fields;

namespace QuickBooks.Net.Payments.Data.Models
{
    public enum ChargeStatus
    {
        Authorized,
        Declined,
        Captured,
        Cancelled,
        Settled,
        Refunded
    }

    public class Charge : QuickBooksPaymentsBaseModelString
    {
        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ChargeStatus Status { get; set; }

        [JsonProperty("amount", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Decimal Amount { get; set; }

        [JsonProperty("currency", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Currency { get; set; }

        [JsonProperty("token", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Token { get; set; }

        [JsonProperty("card", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Card Card { get; set; }

        [JsonProperty("context", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public PaymentContext Context { get; set; }

        [JsonProperty("capture", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool Capture { get; set; }

        [JsonProperty("authCode", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string AuthCode { get; set; }

        [JsonProperty("captureDetail", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public CaptureDetail CaptureDetail { get; set; }

        [JsonProperty("refundDetail", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<Refund> RefundDetails { get; set; }

        [JsonProperty("description", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Description { get; set; }

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
