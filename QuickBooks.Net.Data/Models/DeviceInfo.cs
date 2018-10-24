using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBooks.Net.Data.Models
{
    public class DeviceInfo : QuickBooksBaseModel
    {
        public string Type { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }

        public bool Encrypted { get; set; }

        public string PhoneNumber { get; set; }

        public string MacAddress { get; set; }

        public string IpAddress { get; set; }

        internal override QuickBooksBaseModel CreateReturnObject()
        {
            return this;
        }

        internal override QuickBooksBaseModel UpdateReturnObject()
        {
            return this;
        }

        internal override QuickBooksBaseModel DeleteReturnObject()
        {
            return this;
        }
    }
}
