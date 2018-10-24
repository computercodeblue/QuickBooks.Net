using System;
using Newtonsoft.Json;

namespace QuickBooks.Net.Payments.Data.Models
{
    public enum BankAccountType
    {
        PERSONAL_CHEKCING,
        PERSONAL_SAVINGS
    }

    public enum BankAccountInputType
    {
        KEYED
    }

    public class BankAccount : QuickBooksPaymentsBaseModelString
    {
        [JsonProperty("updated")]
        public DateTime Updated { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("routingNumber")]
        public string RoutingNumber { get; set; }

        [JsonProperty("accountNumber")]
        public string AccountNumber { get; set; }

        [JsonProperty("accountType")]
        public BankAccountType AccountType { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("default")]
        public bool Default { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("inputType")]
        public BankAccountInputType InputType { get; set; }

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
