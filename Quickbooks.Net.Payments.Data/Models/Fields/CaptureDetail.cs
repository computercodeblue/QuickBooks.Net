using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace QuickBooks.Net.Payments.Data.Models.Fields
{
    public class CaptureDetail
    {
        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("context")]
        public PaymentContext Context { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
