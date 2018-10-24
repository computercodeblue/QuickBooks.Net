using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QuickBooks.Net.Payments.Data.Models.Fields;

namespace QuickBooks.Net.Payments.Data.Models
{
    public class Refund : QuickBooksPaymentsBaseModelString
    {
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        
        [JsonProperty("context")]
        public PaymentContext Context { get; set; }

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
