using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyMediaDatabase1.Models
{
    public abstract class Person
    {
        //p
        public int ID { get; set; }

        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", 
            ApplyFormatInEditMode = true,
            NullDisplayText = "Who Knows?")]
        [Display(Name = "Born")]
        public DateTime? DateBorn { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", 
            ApplyFormatInEditMode = true, 
            NullDisplayText = "TBA")]
        [Display(Name = "Died")]
        public DateTime? DateDied { get; set; }
        [StringLength(50)]
        [DisplayFormat(NullDisplayText = "Undetermined")]
        public string Nationality { get; set; }
        [Display(Name = "Name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

    }
}