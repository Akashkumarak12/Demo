using System;
using System.Collections.Generic;

namespace Demo.Models
{
    public partial class Category
    {
        public Category()
        {
            Product1s = new HashSet<Product1>();
        }

        public int Id { get; set; }
        public string? Brand { get; set; }
        public string? SupplierName { get; set; }
        public string? Location { get; set; }

        public virtual ICollection<Product1> Product1s { get; set; }
    }
}
