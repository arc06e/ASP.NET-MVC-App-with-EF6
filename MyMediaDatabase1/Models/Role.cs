using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMediaDatabase1.Models
{
    public class Role
    {
        //p
        public int ID { get; set; }
        public int ContributorID { get; set; }
        public int MovieID { get; set; }
        public string Contribution { get; set; }

        //np one-to-many
        public virtual Contributor Contributor { get; set; }
        public virtual Movie Movie { get; set; }
    }
}