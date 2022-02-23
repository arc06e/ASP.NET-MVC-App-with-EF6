using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyMediaDatabase1.Models
{
    public abstract class Medium
    {
        public int ID { get; set; }
       
        [StringLength(50)]
        public string Title { get; set; }
        
        [StringLength(50)]
        [DisplayFormat(NullDisplayText = "Undetermined")]
        public string Genre { get; set; }
        [Range(1,10000)]
        [DisplayFormat(NullDisplayText = "Unknown")]
        public int? Length { get; set; }
        [Range(1,10000)]
        [Display(Name = "Year Released")]
        [DisplayFormat(NullDisplayText = "Uncertain")]
        public int? YearReleased { get; set; }
    }
}