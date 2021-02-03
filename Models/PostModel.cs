using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace labb2.Models
{
    public class PostModel
    {
        // Properties
        [Required(ErrorMessage = "Oops, you forgot something.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Oops, you forgot something.")]
        public string Todo { get; set; }
    }
}