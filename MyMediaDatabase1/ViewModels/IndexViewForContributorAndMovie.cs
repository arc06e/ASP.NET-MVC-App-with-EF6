using MyMediaDatabase1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMediaDatabase1.ViewModels
{
    public class IndexViewForContributorAndMovie
    {
        public IEnumerable<Contributor> Contributors { get; set; }
        public IEnumerable<Movie> Movies { get; set; }
        public IEnumerable<Role> Roles { get; set; }


    }
}