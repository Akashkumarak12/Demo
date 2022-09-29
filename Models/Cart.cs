using System;
using System.Collections.Generic;

namespace Demo.Models
{
    public partial class Cart
    {
        public int CartId { get; set; }
        public int? Quantity { get; set; }
        public int? UserId { get; set; }
        public int? ProductId { get; set; }

        public virtual Product1? Product { get; set; }
        public virtual User1? User { get; set; }
    }
}
