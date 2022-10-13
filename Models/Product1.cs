using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Demo.Models
{
    public partial class Product1
    {
        public Product1()
        {
            Carts = new HashSet<Cart>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? Image { get; set; }
        public string? Brand { get; set; }
        public int? Price { get; set; }
        public int? Stock { get; set; }
        public int? CategoryId { get; set; }
        public string? Description { get; set; }
        public int? Id { get; set; }
        [Display(Name = "VendorId")]
        public virtual Category? IdNavigation { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
