using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MyMediaDatabase1.Models;
using System.Data.Entity.Migrations;

namespace MyMediaDatabase1.DAL
{
    public class MediaInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<MediaContext>
    {
        protected override void Seed(MediaContext context)
        {
            var authors = new List<Author>
            {
                new Author {FirstName="Dashiell",LastName="Hammett",DateBorn=DateTime.Parse("1894-05-27"),DateDied=DateTime.Parse("1961-01-10"),Nationality="American" },
                new Author {FirstName="Larry",LastName="McMurty",DateBorn=DateTime.Parse("1936-06-03"),DateDied=DateTime.Parse("2021-03-25"),Nationality="American"},
                new Author {FirstName="David",LastName ="Grann",DateBorn=DateTime.Parse("1967-03-10"),Nationality="American"},
                new Author {FirstName="Martha",LastName="Wells",DateBorn=DateTime.Parse("1864-09-01"),Nationality="American"}
            };
            authors.ForEach(a => context.Authors.Add(a));
            context.SaveChanges();

            var books = new List<Book>
            {
                new Book {
                    Title="Red Harvest", 
                    Genre="Crime", 
                    Length=224, 
                    YearReleased=1929,
                    AuthorID = authors.Single(s => s.LastName == "Hammett").ID
                },
                new Book
                {
                    Title="The Maltese Falcon",
                    Genre="Crime",
                    Length=217,
                    YearReleased=1930,
                    AuthorID = authors.Single(s => s.LastName == "Hammett").ID
                },
                new Book
                {
                    Title="The Thin Man",
                    Genre="Crime",
                    Length=259,
                    YearReleased=1934,
                    AuthorID = authors.Single(s => s.LastName == "Hammett").ID
                },
                new Book 
                {
                    Title="Lonesome Dove",
                    Genre="Western",
                    Length=843,
                    YearReleased=1985,
                    AuthorID = authors.Single(s => s.LastName == "McMurty").ID
                },
                new Book
                {
                    Title="Streets of Laredo",
                    Genre="Western",
                    Length=589,
                    YearReleased=1993,
                    AuthorID = authors.Single(s => s.LastName == "McMurty").ID
                },
                new Book
                {
                    Title="Dead Man's Walk",
                    Genre="Western",
                    Length=488,
                    YearReleased=1997,
                    AuthorID = authors.Single(s => s.LastName == "McMurty").ID
                },
                new Book
                {
                    Title="Lost City of Z",
                    Genre="Non-Fiction",
                    Length=352,
                    YearReleased=2009,
                    AuthorID = authors.Single(s => s.LastName == "Grann").ID
                },
                new Book
                {
                    Title="Killers of the Flower Moon",
                    Genre="Non-Fiction",
                    Length=352,
                    YearReleased=2017,
                    AuthorID = authors.Single(s => s.LastName == "Grann").ID
                },
                new Book 
                { 
                    Title="All Systems Red", 
                    Genre="Sci-Fi",
                    Length=152, 
                    YearReleased=2017,
                    AuthorID = authors.Single(s => s.LastName == "Wells").ID
                },
                new Book
                {
                    Title="Artificial Condition",
                    Genre="Sci-Fi",
                    Length=158,
                    YearReleased=2018,
                    AuthorID = authors.Single(s => s.LastName == "Wells").ID
                },
                new Book
                {
                    Title="Rogue Protocol",
                    Genre="Sci-Fi",
                    Length=158,
                    YearReleased=2018,
                    AuthorID = authors.Single(s => s.LastName == "Wells").ID
                },
                new Book
                {
                    Title="Exit Strategy",
                    Genre="Sci-Fi",
                    Length=176,
                    YearReleased=2018,
                    AuthorID = authors.Single(s => s.LastName == "Wells").ID
                },
            };
            books.ForEach(b => context.Books.Add(b));
            context.SaveChanges();

            var contributors = new List<Contributor>
            {
                new Contributor { FirstName="William",LastName="Wellman", DateBorn=DateTime.Parse("1896-02-29"),DateDied=DateTime.Parse("1975-12-09"),Nationality="American"},
                new Contributor { FirstName="Harvey", LastName="Thew", DateBorn=DateTime.Parse("1883-07-04"),DateDied=DateTime.Parse("1946-11-06"),Nationality="American"},
                new Contributor { FirstName="James",LastName="Cagney",DateBorn=DateTime.Parse("1899-07-17"),DateDied=DateTime.Parse("1986-03-30"),Nationality="American"},
                new Contributor { FirstName="Jean",LastName="Harlow",DateBorn=DateTime.Parse("1911-03-03"),DateDied=DateTime.Parse("1937-06-07"),Nationality="American"},
                new Contributor { FirstName="Billy",LastName="Wilder",DateBorn=DateTime.Parse("1906-06-22"),DateDied=DateTime.Parse("2002-03-27"),Nationality="Austrian-American"},
                new Contributor { FirstName="Charles",LastName="Brackett",DateBorn=DateTime.Parse("1892-11-26"),DateDied=DateTime.Parse("1969-03-9"),Nationality="American"},
                new Contributor { FirstName="D.M.",LastName="Marshman Jr.",DateBorn=DateTime.Parse("1922-12-21"),DateDied=DateTime.Parse("2015-09-17"),Nationality="American"},
                new Contributor { FirstName="Gloria",LastName="Swanson",DateBorn=DateTime.Parse("1899-03-27"),DateDied=DateTime.Parse("1983-04-04"),Nationality="American"},
                new Contributor { FirstName="William",LastName="Holden",DateBorn=DateTime.Parse("1918-04-17"),DateDied=DateTime.Parse("1981-11-12"),Nationality="American"},
                new Contributor { FirstName="Blake",LastName="Edwards",DateBorn=DateTime.Parse("1922-07-26"),DateDied=DateTime.Parse("2010-12-15"),Nationality="American"},
                new Contributor { FirstName="William Peter",LastName="Blatty",DateBorn=DateTime.Parse("1928-01-07"),DateDied=DateTime.Parse("2017-01-12"),Nationality="American"},
                new Contributor { FirstName="Peter",LastName="Sellers",DateBorn=DateTime.Parse("1925-08-08"),DateDied=DateTime.Parse("1980-07-24"),Nationality="English"},
                new Contributor { FirstName="Elke",LastName="Sommer",DateBorn=DateTime.Parse("1940-11-05"),Nationality="German"},
                new Contributor { FirstName="Stanley",LastName="Kubrick",DateBorn=DateTime.Parse("1928-07-26"),DateDied=DateTime.Parse("1999-03-07"),Nationality="American"},
                new Contributor { FirstName="Diane",LastName="Johnson",DateBorn=DateTime.Parse("1934-04-28"),Nationality="American"},
                new Contributor { FirstName="Jack",LastName="Nicholson",DateBorn=DateTime.Parse("1937-04-22"),Nationality="American"},
                new Contributor { FirstName="Shelley",LastName="Duvall",DateBorn=DateTime.Parse("1949-07-07"),Nationality="American"},
                new Contributor { FirstName="Clint",LastName="Eastwood",DateBorn=DateTime.Parse("1930-05-21"),Nationality="American"},
                new Contributor { FirstName="David Webb",LastName="Peoples",DateBorn=DateTime.Parse("1940-02-09"),Nationality="American"},
                new Contributor { FirstName="Gene",LastName="Hackman",DateBorn=DateTime.Parse("1930-01-03"),Nationality="American"},
                new Contributor { FirstName="Morgan",LastName="Freeman",DateBorn=DateTime.Parse("1937-05-01"),Nationality="American"},
                new Contributor { FirstName="Richard",LastName="Harris",DateBorn=DateTime.Parse("1930-10-01"),DateDied=DateTime.Parse("2002-10-25"),Nationality="Irish"},
            };
            contributors.ForEach(c => context.Contributors.AddOrUpdate(p => p.LastName, c));
            context.SaveChanges();

            var movies = new List<Movie>
            {
                new Movie { Title="A Public Enemy",Genre="Crime",Length=83,YearReleased=1931},
                new Movie { Title="Sunset Boulevard", Genre="Drama", Length=110, YearReleased=1950},
                new Movie { Title="A Shot in the Dark", Genre="Comedy", Length=102, YearReleased=1964},
                new Movie { Title="The Shining", Genre="Horror", Length=144, YearReleased=1980},
                new Movie { Title="Unforgiven", Genre="Western", Length=131, YearReleased=1992}
            };
            movies.ForEach(m => context.Movies.AddOrUpdate(p => p.Title, m));
            context.SaveChanges();

            var roles = new List<Role>
            {
                new Role 
                {
                    ContributorID = contributors.Single(s => s.LastName == "Wellman").ID,
                    MovieID = movies.Single(c => c.Title == "A Public Enemy" ).ID,
                    Contribution = "Director"
                },
                new Role
                {
                    ContributorID = contributors.Single(s => s.LastName == "Thew").ID,
                    MovieID = movies.Single(c => c.Title == "A Public Enemy" ).ID,
                    Contribution = "Writer"
                },
                new Role
                {
                    ContributorID = contributors.Single(s => s.LastName == "Cagney").ID,
                    MovieID = movies.Single(c => c.Title == "A Public Enemy" ).ID,
                    Contribution = "Actor"
                },
                new Role
                {
                    ContributorID = contributors.Single(s => s.LastName == "Harlow").ID,
                    MovieID = movies.Single(c => c.Title == "A Public Enemy" ).ID,
                    Contribution = "Actor"
                },
                new Role
                {
                    ContributorID = contributors.Single(s => s.LastName == "Wilder").ID,
                    MovieID = movies.Single(c => c.Title == "Sunset Boulevard" ).ID,
                    Contribution = "Director"
                },
                new Role
                {
                    ContributorID = contributors.Single(s => s.LastName == "Wilder").ID,
                    MovieID = movies.Single(c => c.Title == "Sunset Boulevard" ).ID,
                    Contribution = "Writer"
                },
                new Role
                {
                    ContributorID = contributors.Single(s => s.LastName == "Brackett").ID,
                    MovieID = movies.Single(c => c.Title == "Sunset Boulevard" ).ID,
                    Contribution = "Writer"
                },
                new Role
                {
                    ContributorID = contributors.Single(s => s.LastName == "Marshman Jr.").ID,
                    MovieID = movies.Single(c => c.Title == "Sunset Boulevard" ).ID,
                    Contribution = "Director"
                },
                new Role
                {
                    ContributorID = contributors.Single(s => s.LastName == "Swanson").ID,
                    MovieID = movies.Single(c => c.Title == "Sunset Boulevard" ).ID,
                    Contribution = "Actor"
                },
                new Role
                {
                    ContributorID = contributors.Single(s => s.LastName == "Holden").ID,
                    MovieID = movies.Single(c => c.Title == "Sunset Boulevard" ).ID,
                    Contribution = "Actor"
                },
                new Role
                {
                    ContributorID = contributors.Single(s => s.LastName == "Edwards").ID,
                    MovieID = movies.Single(c => c.Title == "A Shot in the Dark" ).ID,
                    Contribution = "Director"
                },
                new Role
                {
                    ContributorID = contributors.Single(s => s.LastName == "Edwards").ID,
                    MovieID = movies.Single(c => c.Title == "A Shot in the Dark" ).ID,
                    Contribution = "Writer"
                },
                new Role
                {
                    ContributorID = contributors.Single(s => s.LastName == "Blatty").ID,
                    MovieID = movies.Single(c => c.Title == "A Shot in the Dark" ).ID,
                    Contribution = "Writer"
                },
                new Role
                {
                    ContributorID = contributors.Single(s => s.LastName == "Sellers").ID,
                    MovieID = movies.Single(c => c.Title == "A Shot in the Dark" ).ID,
                    Contribution = "Actor"
                },
                new Role
                {
                    ContributorID = contributors.Single(s => s.LastName == "Sommer").ID,
                    MovieID = movies.Single(c => c.Title == "A Shot in the Dark" ).ID,
                    Contribution = "Actor"
                },
                new Role
                {
                    ContributorID = contributors.Single(s => s.LastName == "Kubrick").ID,
                    MovieID = movies.Single(c => c.Title == "The Shining" ).ID,
                    Contribution = "Director"
                },
                new Role
                {
                    ContributorID = contributors.Single(s => s.LastName == "Kubrick").ID,
                    MovieID = movies.Single(c => c.Title == "The Shining" ).ID,
                    Contribution = "Writer"
                },
                new Role
                {
                    ContributorID = contributors.Single(s => s.LastName == "Johnson").ID,
                    MovieID = movies.Single(c => c.Title == "The Shining" ).ID,
                    Contribution = "Writer"
                },
                new Role
                {
                    ContributorID = contributors.Single(s => s.LastName == "Nicholson").ID,
                    MovieID = movies.Single(c => c.Title == "The Shining" ).ID,
                    Contribution = "Actor"
                },
                new Role
                {
                    ContributorID = contributors.Single(s => s.LastName == "Duvall").ID,
                    MovieID = movies.Single(c => c.Title == "The Shining" ).ID,
                    Contribution = "Actor"
                },
                new Role
                {
                    ContributorID = contributors.Single(s => s.LastName == "Eastwood").ID,
                    MovieID = movies.Single(c => c.Title == "Unforgiven" ).ID,
                    Contribution = "Director"
                },
                new Role
                {
                    ContributorID = contributors.Single(s => s.LastName == "Peoples").ID,
                    MovieID = movies.Single(c => c.Title == "Unforgiven" ).ID,
                    Contribution = "Writer"
                },
                new Role
                {
                    ContributorID = contributors.Single(s => s.LastName == "Eastwood").ID,
                    MovieID = movies.Single(c => c.Title == "Unforgiven" ).ID,
                    Contribution = "Actor"
                },
                new Role
                {
                    ContributorID = contributors.Single(s => s.LastName == "Hackman").ID,
                    MovieID = movies.Single(c => c.Title == "Unforgiven" ).ID,
                    Contribution = "Actor"
                },
                new Role
                {
                    ContributorID = contributors.Single(s => s.LastName == "Freeman").ID,
                    MovieID = movies.Single(c => c.Title == "Unforgiven" ).ID,
                    Contribution = "Actor"
                },
                new Role
                {
                    ContributorID = contributors.Single(s => s.LastName == "Harris").ID,
                    MovieID = movies.Single(c => c.Title == "Unforgiven" ).ID,
                    Contribution = "Actor"
                }
            };

            foreach (Role r in roles)
            {
                var roleInDataBase = context.Roles.Where(
                    x =>
                         x.Contributor.ID == r.ContributorID &&
                         x.Movie.ID == r.MovieID).SingleOrDefault();
                if (roleInDataBase == null)
                {
                    context.Roles.Add(r);
                }
            }
            context.SaveChanges();

        }
    }
}