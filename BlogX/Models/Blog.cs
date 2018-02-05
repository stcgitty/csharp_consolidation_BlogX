using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogX.Models
{
    public class Blog
    {
        [Required]
        public string Title { get; set; }
        public string Content { get; set; }
        public int Id { get; set; }
    }
}