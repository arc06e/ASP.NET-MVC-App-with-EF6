using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyMediaDatabase1.Models
{
    public class Movie : Medium
    {
        //p -no unique properties

        //np many-to-one
        public virtual ICollection<Role> Roles { get; set; }


    }
}