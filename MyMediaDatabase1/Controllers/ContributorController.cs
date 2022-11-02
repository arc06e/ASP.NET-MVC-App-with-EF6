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
    public class ContributorController : Controller
    {
        private MediaContext db = new MediaContext();
        
        public ActionResult Index(int? id)
        {
            //The code begins by creating an instance of the view model        
            var viewModel = new IndexViewForContributorAndMovie();
            //and putting in it the list of Contributors.
            viewModel.Contributors = db.Contributors
                .Include(i => i.Roles)
                .OrderBy(i => i.LastName);
            
            //The code specifies eager loading for the Contributors.Roles navigation property
            //eager loading is not required but is done to improve performance.

            //If an contributor's ID was selected, the selected contributor is retrieved from the list of contributors in the view model.        
            if (id != null)
            {
                ViewBag.ContributorId = id.Value;
                //The view model's Roles property is then loaded with the Role entities from that Contributors's Roles navigation property.
                //The Where method returns a collection,
                //but in this case the criteria passed to that method result in only a single Contributor entity being returned.
                viewModel.Roles = viewModel.Contributors.Where(
                    //The Single method converts the collection into a single Contributor entity,
                    //which gives you access to that entity's Roles property.  
                    i => i.ID == id.Value).Single().Roles;
                //You use the Single method on a collection when you know the collection will have only one item.
                //The Single method throws an exception if the collection passed to it is empty or if there's more than one item

                //The Single method throws an exception if the collection passed to it is empty or if there's more than one item.
                //An alternative is SingleOrDefault, which returns a default value (null in this case) if the collection is empty.
                //However, in this case that would still result in an exception (from trying to find a Contributors property on a null reference),
                //and the exception message would less clearly indicate the cause of the problem. 
            }
            return View(viewModel);
        }

        // GET: Contributor/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Contributor contributor = await db.Contributors.FindAsync(id);

            var distinct = new HashSet<string>(contributor.Roles.Select(c => c.Contribution));
            distinct.Distinct().ToList();
            ViewBag.Roles = distinct;

            if (contributor == null)
            {
                return HttpNotFound();
            }
            return View(contributor);
        }

        // GET: Contributor/Create
        public async Task<ActionResult> Create(int? id)
        {
            MovieRolesData viewModel = new MovieRolesData();
            viewModel.ItemList = new SelectList(db.Movies, "ID", "Title");

            if(id != null) 
            {
                Contributor contributor = await db.Contributors.FindAsync(id);
                viewModel.Role = new Role();   
                viewModel.Role.ContributorID = contributor.ID;
            }

            return View(viewModel);
        }

        // POST: Contributor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MovieRolesData viewModel)
        {
            //contributor can be added to db by being contained in role's contributor NP
            
            Role role = new Role();
            try
            {
                //checks if creating new contributor
                if (viewModel.Role.ContributorID == 0)
                {
                    Contributor contributor = new Contributor();
                    role.Contributor = contributor;

                    contributor.LastName = viewModel.Contributor.LastName;
                    contributor.FirstName = viewModel.Contributor.FirstName;
                    contributor.DateBorn = viewModel.Contributor.DateBorn;
                    contributor.DateDied = viewModel.Contributor.DateDied;
                    contributor.Nationality = viewModel.Contributor.Nationality;

                    if (viewModel.Contributor.LastName == null || viewModel.Contributor.FirstName == null)
                    {
                        viewModel.Role = null;
                        viewModel.ItemList = new SelectList(db.Movies, "ID", "Title");
                        ModelState.AddModelError("", "You need to add at least a first name and a last name");
                        return View(viewModel);
                    }
                }

                if (viewModel.Role.Contribution != null)
                {
                    //or adding new role to existing contributor:
                    role.Contribution = viewModel.Role.Contribution;
                    role.ContributorID = viewModel.Role.ContributorID;
                }
                //loads selected movieID from dropdown list
                string selectedValue = viewModel.SelectedItem;
                int n = -1;
                //if adding new role to existing movie
                if (selectedValue != null)
                {
                    int.TryParse(selectedValue, out n);
                    role.MovieID = n;
                }

                //if creating new contributor without any roles
                if (role.MovieID != n && viewModel.Movie.Title == null)
                {
                    //this breaks out of using roles to simply adding a new contributor instance
                    Contributor contributor = new Contributor();
                    contributor.LastName = viewModel.Contributor.LastName;
                    contributor.FirstName = viewModel.Contributor.FirstName;
                    contributor.DateBorn = viewModel.Contributor.DateBorn;
                    contributor.DateDied = viewModel.Contributor.DateDied;
                    contributor.Nationality = viewModel.Contributor.Nationality;
                    if (ModelState.IsValid)
                    {
                        db.Contributors.Add(contributor);
                       await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                }

                //or also adding new role AND new movie at the same time
               if(!(role.MovieID == n))
                { 
                    //this adds new movie to role's movie NP
                    Movie movie = new Movie();
                    role.Movie = movie;
                    role.Movie.Title = viewModel.Movie.Title;
                }

                if (viewModel.Role.Contribution == null)
                {
                    if(viewModel.Role.ContributorID == 0)
                    {
                        viewModel.Role = null;
                    }
                    
                    viewModel.ItemList = new SelectList(db.Movies, "ID", "Title");
                    ModelState.AddModelError("", "If you going to add a role, you must have a contribution.");
                    return View(viewModel);
                }

                if (selectedValue != null && !(role.MovieID == n))
                {
                    viewModel.ItemList = new SelectList(db.Movies, "ID", "Title");
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
            return View();
        }


        // GET: Contributor/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Contributor contributor = await db.Contributors.FindAsync(id);

            if (contributor == null)
            {
                return HttpNotFound();
            }
            return View(contributor);
        }

        // POST: Contributor/Edit/5
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

            var contributorToUpdate = await db.Contributors.FindAsync(id);

            if (TryUpdateModel(contributorToUpdate, "",
                new string[]
                { "LastName","FirstName","DateBorn","DateDied","Nationality" }))
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

            return View(contributorToUpdate);
        }



        // GET: Contributor/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }



            Contributor contributor = await db.Contributors.FindAsync(id);

            if (contributor == null)
            {
                return HttpNotFound();
            }
            return View(contributor);
        }


        // GET: Contributor/Delete/5
        public async Task<ActionResult> DeleteRole(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Role role = await db.Roles.FindAsync(id);

            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }


        // POST: Contributor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        //public ActionResult DeleteConfirmed(int id)
        {
            Contributor contributor = await db.Contributors.FindAsync(id);
            db.Contributors.Remove(contributor);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }



        // POST: Contributor/Delete/5
        [HttpPost, ActionName("DeleteRole")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteRoleConfirmed(int id)
        //public ActionResult DeleteRoleConfirmed(int id)
        {
            Role role = await db.Roles.FindAsync(id);
            db.Roles.Remove(role);
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
