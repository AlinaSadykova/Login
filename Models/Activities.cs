using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Login.Models{


public class Activities{

        [Key]
        public int activitiesid { get; set; }
        [Required]
        [MinLength(2)]
        public string title { get; set; }
        [Required]
        [FutureDate(ErrorMessage="Date should be in the future.")]
        public DateTime date { get; set; }
        [Required]
        public int numduration { get; set; }
        [Required]
        public string duration { get; set; }
        [Required]
        [MinLength(10)]
        public string description { get; set; }
     
        public int eusersid {get; set; }
        public Eusers eusers { get; set; }
        public List<Guestlist> persons{ get; set; }
    }
    public class FutureDateAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        bool valid = false;
        if ((DateTime)value >= DateTime.Now) {
            valid = true;
        }
        return valid; 
        
    }
} 
}