using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyMediaDatabase1.Models
{
    public class Author : Person
    {
        //p - no unique properties
        

        //np one-to-many -- no fk
        public virtual ICollection<Book> Books { get; set; }
    }
}



