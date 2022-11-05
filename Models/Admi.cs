using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Demo.Models
{
    public partial class Admi
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "*")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "*")]
        public string? Password { get; set; }
        [Compare("Password", ErrorMessage = "Password do not matched")]
        [NotMapped]
        [Display(Name = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }

    }
}
