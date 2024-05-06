using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UserDetails.Models
{
    public class Genders
    {
        [Key]
        public int GenderId { get; set; }
        public string GenderName { get; set; }
        
    }
}