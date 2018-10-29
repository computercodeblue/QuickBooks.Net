using System;
using Newtonsoft.Json;

namespace QuickBooks.Net.Payments.Data.Models.Fields
{
    public class CvcVerification
    {
        [JsonProperty("date", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime Date { get; set; }

        [JsonProperty("result", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Result { get; set; }
    }
}
