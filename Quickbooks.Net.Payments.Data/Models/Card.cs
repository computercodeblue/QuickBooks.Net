using System;
using Newtonsoft.Json;

using QuickBooks.Net.Payments.Data.Models.Fields;

namespace QuickBooks.Net.Payments.Data.Models
{
    public enum CardType
    {
        VISA,
        MC,
        AMEX,
        DISC,
        DINERS,
        JCB
    }

    public class Card : QuickBooksPaymentsBaseModelString
    {
        [JsonProperty("updated")]
        public DateTime Updated { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("expMonth")]
        public string ExpMonth { get; set; }

        [JsonProperty("expYear")]
        public string ExpYear { get; set; }

        [JsonProperty("cvc")]
        public string Cvc { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address")]
        public CardholderAddress Address { get; set; }

        [JsonProperty("default")]
        public bool Default { get; set; }

        [JsonProperty("commercialCardCode")]
        public string CommercialCardCode { get; set; }

        [JsonProperty("cvcVerification")]
        public string CvcVerification { get; set; }

        [JsonProperty("isBusiness")]
        public bool IsBusiness { get; set; }

        [JsonProperty("cardType")]
        public CardType CardType { get; set; }

        [JsonProperty("capture")]
        public bool Capture { get; set; }

        [JsonProperty("authCode")]
        public string AuthCode { get; set; }

        [JsonProperty("description")]
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
