﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public partial class OrderMaster
    {
        public OrderMaster()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderMasterid { get; set; }
        [DataType(DataType.Date)]
        public DateTime Orderdate { get; set; }
        public int TotalAmount { get; set; }
        public int CardNumber { get; set; }
        public int? AmountPaid { get; set; }
        public int? Userid { get; set; }

        public virtual User1? User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}