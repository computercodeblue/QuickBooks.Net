using Newtonsoft.Json;
using QuickBooks.Net.Payments.Data.Models.Fields;

namespace QuickBooks.Net.Payments.Data.Models
{
    public enum ECheckStatusType
    {
        PENDING,
        SUCCEEDED,
        DECLINED,
        VOIDED,
        REFUNDED
    }

    public enum PaymentModeType
    {
        WEB
    }

    public class ECheck : QuickBooksPaymentsBaseModelString
    {
        [JsonProperty("authCode")]
        public string AuthCode { get; set; }

        [JsonProperty("status")]
        public ECheckStatusType Status { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("bankAccount")]
        public BankAccount BankAccount { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("bankAccountOnFile")]
        public string BankAccountOnFile { get; set; }

        [JsonProperty("context")]
        public PaymentContext context { get; set; }

        [JsonProperty("paymentMode")]
        public PaymentModeType PaymentMode { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("checkNumber")]
        public string CheckNumber { get; set; }

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
