using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Login.Models{


public class Eusers{

        [Key]
        public int eusersid { get; set; }
        
        public string firstname { get; set; }
       
        public string lastname { get; set; }
     
        public string email { get; set; }
       
        public string password { get; set; }

        public List<Activities> Activities { get; set; }
        public Eusers()
        {
            Activities = new List<Activities>();
        }
        
     
    }
}