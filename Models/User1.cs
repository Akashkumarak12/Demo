using System;
using System.Collections.Generic;

namespace Demo.Models
{
    public partial class User1
    {
        public User1()
        {
            Carts = new HashSet<Cart>();
        }

        public int UserId { get; set; }
        public string? UserName { get; set; }
        public int? MobileNo { get; set; }
        public string? EmailId { get; set; }
        public string? Password { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
    }
}
