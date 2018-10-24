using System;
using Newtonsoft.Json;

namespace QuickBooks.Net.Payments.Data.Models
{
    public abstract class QuickBooksPaymentsBaseModel
    {
        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Id { get; set; }

        [JsonProperty("created", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime Created { get; set; }

        internal abstract QuickBooksPaymentsBaseModel CreateReturnObject();
        internal abstract QuickBooksPaymentsBaseModel UpdateReturnObject();
        internal abstract QuickBooksPaymentsBaseModel DeleteReturnObject();
    }
}
