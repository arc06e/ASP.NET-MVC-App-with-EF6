using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyMediaDatabase1.Models
{
    public class Contributor : Person
    {
        //p - no unique properties
     
        //np one-to-many - no fk
        public virtual ICollection<Role> Roles { get; set; }
    }
}
