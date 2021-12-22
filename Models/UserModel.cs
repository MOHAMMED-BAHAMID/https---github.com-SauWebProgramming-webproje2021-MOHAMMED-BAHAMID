using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace WebProje.Models
{
     public class User : IdentityUser
    {
        public string TC { get; set; }

        [Display(Name="Ad")]
        public string FName { get; set; }

        [Display(Name="Soyad")]
        public string LName { get; set; }

        [Display(Name="Cinsiyet")]
        public char Gender { get; set; }

        [Display(Name="Şehir")]
        public string City { get; set; }
    }

}


//  sV7D$A3m
