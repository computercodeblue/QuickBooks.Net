using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using QuickBooks.Net.Data.Models.Fields;

namespace QuickBooks.Net.Data.Models
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

    public class Charge : QuickBooksBaseModelString
    {
        public DateTime Created { get; set; }

        public ChargeStatus Status { get; set; }

        public Decimal Amount { get; set; }

        public string Currency { get; set; }

        public string Token { get; set; }

        public Card Card { get; set; }

        public PaymentContext Context { get; set; }

        public bool Capture { get; set; }

        public string AuthCode { get; set; }
        
        [JsonProperty("RefundDetail")]
        public List<Refund> RefundDetails { get; set; }

        public string Description { get; set; }

        internal override QuickBooksBaseModelString CreateReturnObject()
        {
            return this;
        }

        internal override QuickBooksBaseModelString UpdateReturnObject()
        {
            return this;
        }

        internal override QuickBooksBaseModelString DeleteReturnObject()
        {
            return this;
        }
    }
}
