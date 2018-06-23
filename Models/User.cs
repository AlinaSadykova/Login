using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Login.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Login.Models{


public class User{
        [Key]
        public int ideusers { get; set; }
        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "User name can only contain letters")]
        public string firstname { get; set; }
        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "User name can only contain letters")]
        public string lastname { get; set; }
        [Required]
        [EmailAddress]
        
        public string email { get; set; }
        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Password must contain at least one number, one letter and one special character")]
        [MinLength(8)]
        [DataType(DataType.Password)] 
        public string password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "passwords don't match")]
        public string confpassword { get; set; }
    }

}

