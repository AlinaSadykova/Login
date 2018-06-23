using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using Login.Models;


public class Guestlist
{
    [Key]
    public int guestlistid {get; set;}
    
    public int eusersid {get; set;}
    public Eusers eusers {get; set; }

    public int activitiesid { get; set; }

    public Activities activities {get; set;}
    
}