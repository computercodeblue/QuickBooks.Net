using System;
using Newtonsoft.Json;

using QuickBooks.Net.Payments.Data.Models.Fields;

namespace QuickBooks.Net.Payments.Data.Models
{
    public class CardObject
    {
        [JsonProperty("card", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Card Card { get; set; }
    }

    public class Card : QuickBooksPaymentsBaseModelString
    {
        [JsonProperty("updated", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime Updated { get; set; }

        [JsonProperty("number", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Number { get; set; }

        [JsonProperty("expMonth", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ExpMonth { get; set; }

        [JsonProperty("expYear", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ExpYear { get; set; }

        [JsonProperty("cvc", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Cvc { get; set; }

        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("address", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public CardholderAddress Address { get; set; }

        [JsonProperty("default", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool Default { get; set; }

        [JsonProperty("commercialCardCode", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string CommercialCardCode { get; set; }

        [JsonProperty("cvcVerification", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public CvcVerification CvcVerification { get; set; }

        [JsonProperty("isBusiness", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool IsBusiness { get; set; }

        [JsonProperty("cardType", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string CardType { get; set; }

        [JsonProperty("capture", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool Capture { get; set; }

        [JsonProperty("authCode", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string AuthCode { get; set; }

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
