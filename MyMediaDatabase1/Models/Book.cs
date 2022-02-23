using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyMediaDatabase1.Models
{

    public class Book : Medium
    { 
        //p - no unique properties  

        //fk
        public int AuthorID { get; set; }
        //np many-to-one
        public virtual Author Author { get; set; }
        

    }
}