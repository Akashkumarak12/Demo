using System;
using System.Collections.Generic;

namespace Demo.Models
{
    public partial class Cart
    {
        public int Id { get; set; }
        public int? Cartid { get; set; }
        public int? Quantity { get; set; }
        public int? TotalAmount { get; set; }
        public int? Userid { get; set; }
        public int? Productid { get; set; }

        public virtual Product1? Product { get; set; }
        public virtual User1? User { get; set; }
    }
}
