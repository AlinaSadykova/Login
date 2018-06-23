using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Login.Models{


public class Guy{ 

        public int guyid {get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
     
    }
}