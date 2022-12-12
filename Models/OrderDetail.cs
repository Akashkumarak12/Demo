using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Demo.Models
{
    public partial class OrderDetail
    {
        public int Orderid { get; set; }
        [Display(Name = "Total Amount")]
        public int? TotalAmount { get; set; }
        public int? OrderMasterid { get; set; }
        public int? Productid { get; set; }
        public int? Userid { get; set; }

        public virtual OrderMaster? OrderMaster { get; set; }
        public virtual Product1? Product { get; set; }
        public virtual User1? User { get; set; }
    }
}
