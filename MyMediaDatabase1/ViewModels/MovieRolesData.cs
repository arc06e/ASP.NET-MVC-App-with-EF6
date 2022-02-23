using MyMediaDatabase1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMediaDatabase1.ViewModels
{
    public class MovieRolesData
    {
        public Movie Movie { get; set; }
        public Contributor Contributor { get; set; }
        public Role Role { get; set; }

        public SelectList ItemList { get; set; }   
        public string SelectedItem { get; set; }   
    }
}



