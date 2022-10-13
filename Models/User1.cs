using System;
using System.Collections.Generic;

namespace Demo.Models
{
    public partial class User1
    {
        public User1()
        {
            Carts = new HashSet<Cart>();
            OrderDetails = new HashSet<OrderDetail>();
            OrderMasters = new HashSet<OrderMaster>();
        }

        public int UserId { get; set; }
        public string? UserName { get; set; }
        public int? MobileNo { get; set; }
        public string? EmailId { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        public string? Pincode { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<OrderMaster> OrderMasters { get; set; }
    }
}
