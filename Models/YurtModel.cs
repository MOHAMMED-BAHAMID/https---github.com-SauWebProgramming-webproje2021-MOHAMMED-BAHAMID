using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProje.Models
{
    public class Yurt
    {
        [Key][Required]
        public int YurtID { get; set; }

        [Required]
        public string Yurt_Name { get; set; }

        [Required]
        public string address { get; set; }

        [Required]
        public char Type { get; set; }

        public int Price { get; set; }

        public int Available_place { get; set; }

        [Required]
        public string contract { get; set; }

        [Required]
        public string Details { get; set; }

        public List<YurtFoto> YurtFotos { get; set; }
    }


    public class YurtFoto
    {

        [Key][Required]
        public int FotoID { get; set; }
        
        [Required]
        public string FotoDir { get; set; }

        public int YurtID {get; set;}

        [ForeignKey("YurtID")]
        public Yurt Yurt {get; set;}
    }
}
