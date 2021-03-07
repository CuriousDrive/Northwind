using System;
using System.Collections.Generic;

#nullable disable

namespace Northwind_API.Models
{
    public partial class Shipper
    {
        public int ShipperId { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
    }
}
