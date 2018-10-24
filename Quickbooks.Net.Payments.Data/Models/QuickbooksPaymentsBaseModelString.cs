using System;
using Newtonsoft.Json;

namespace QuickBooks.Net.Payments.Data.Models
{
    public abstract class QuickBooksPaymentsBaseModelString
    {
        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("created", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime Created { get; set; }

        internal abstract QuickBooksPaymentsBaseModelString CreateReturnObject();
        internal abstract QuickBooksPaymentsBaseModelString UpdateReturnObject();
        internal abstract QuickBooksPaymentsBaseModelString DeleteReturnObject();
    }
}
