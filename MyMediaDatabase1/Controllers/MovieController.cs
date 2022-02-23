using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MyMediaDatabase1.DAL;
using MyMediaDatabase1.Models;
using MyMediaDatabase1.ViewModels;

namespace MyMediaDatabase1.Controllers
{
    public class MovieController : Controller
    {
        private MediaContext db = new MediaContext();

        public ActionResult Index(int? id)
        {   
            var viewModel = new IndexViewForContributorAndMovie();
                viewModel.Movies = db.Movies
                .Include(i => i.Roles)
                .OrderBy(i => i.YearReleased);
                 
            if (id != null)
            {
                ViewBag.MovieId = id.Value;
                viewModel.Roles = viewModel.Movies
                    .Where(i => i.ID == id.Value)
                    .Single().Roles; 
                    
            }
            return View(viewModel);
        }

        // GET: Movie/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Movie movie = await db.Movies.FindAsync(id);

            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        public async Task<ActionResult> Create(int? id)
        {
            MovieRolesData viewModel = new MovieRolesData();
            viewModel.ItemList = new SelectList(db.Contributors, "ID", "FullName");

            if (id != null)
            {
                Movie movie = await db.Movies.FindAsync(id);
                viewModel.Role = new Role();
                viewModel.Role.MovieID = movie.ID;
            }
            return View(viewModel);
        }


        // POST: Movie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MovieRolesData viewModel)
        {
            Role role = new Role();
            try
            {
                //checks if creating a new movie (sees if view has passed any values to movie ViewModel)
                if (viewModel.Movie != null)
                {
                    Movie movie = new Movie();
                    movie.Title = viewModel.Movie.Title;
                    movie.Genre = viewModel.Movie.Genre;
                    movie.Length = viewModel.Movie.Length;
                    movie.YearReleased = viewModel.Movie.YearReleased;

                    if (movie.Title == null)
                    {
                        ModelState.AddModelError("", "Movie must at least have a title");
                        return View(viewModel);
                    }
                    if (ModelState.IsValid)
                    {
                        db.Movies.Add(movie);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                }

                //loads selected ContributorID from dropdown list
                string selectedValue = viewModel.SelectedItem;
                int n = -1;
                //if adding new role to existing contributor
                if (selectedValue != null)
                {
                    int.TryParse(selectedValue, out n);
                    role.ContributorID = n;
                }
                //checks if adding a new contributor to existing movie
                if (role.ContributorID != n)
                {
                    Contributor contributor = new Contributor();
                    role.Contributor = contributor;

                    role.Contributor.LastName = viewModel.Contributor.LastName;
                    role.Contributor.FirstName = viewModel.Contributor.FirstName;

                }

                role.MovieID = viewModel.Role.MovieID;
                role.Contribution = viewModel.Role.Contribution;

                if (viewModel.Role.Contribution == null)
                {
                    viewModel.ItemList = new SelectList(db.Contributors, "ID", "FullName");
                    ModelState.AddModelError("", "If you going to add a role, you must have a contribution.");
                    return View(viewModel);
                }

                if (selectedValue!=null && viewModel.Contributor.FirstName != null || viewModel.Contributor.LastName != null)
                {
                    viewModel.ItemList = new SelectList(db.Contributors, "ID", "FullName");
                    ModelState.AddModelError("", "You can't simultaneously submit a new and existing contributor");
                    return View(viewModel);
                }

                if (ModelState.IsValid)
                {
                    db.Roles.Add(role);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }


            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            //return View(movie);
            return View(viewModel);
        }


        // GET: Movie/Edit/5
        public async Task <ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Movie movie = await db.Movies.FindAsync(id);

            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var movieToUpdate = await db.Movies.FindAsync(id);

            if (TryUpdateModel(movieToUpdate, "",
                new string[]
                { "Title","Genre","Length","YearReleased" }))
            {

                try
                {
                    await db.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            return View(movieToUpdate);
        }







        // GET: Movie/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Movie movie = await db.Movies.FindAsync(id);

            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Movie movie = await db.Movies.FindAsync(id);
            db.Movies.Remove(movie);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
