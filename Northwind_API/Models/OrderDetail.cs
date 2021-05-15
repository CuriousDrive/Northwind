using System;
using System.Collections.Generic;

#nullable disable

namespace Northwind.Models
{
    public partial class Orderdetail
    {
        public int OrderDetailsId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public float Discount { get; set; }
    }
}
