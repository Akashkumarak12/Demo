using System;
using System.Collections.Generic;

namespace Demo.Models
{
    public partial class OrderDetail
    {
        public int Orderid { get; set; }
        public int? TotalAmount { get; set; }
        public int? OrderMasterid { get; set; }
        public int? Productid { get; set; }

        public virtual OrderMaster? OrderMaster { get; set; }
        public virtual Product1? Product { get; set; }
    }
}
