using MyMediaDatabase1.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace MyMediaDatabase1.DAL
{
    public class MediaContext : DbContext 
    {
        public MediaContext() : base("MediaContext")
        {
        }
        public DbSet<Contributor> Contributors { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Movie> Movies { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //customize some of the mapping using fluent API calls.
            //The API is "fluent" because it's often used by stringing a series of method calls together
            //into a single statement, as in the following example:

            // configures the many-to-many join table:
            //For the many-to-many relationship between the Contributor and Movie entities,
            //the code specifies the table and column names for the join table
            //modelBuilder.Entity<Movie>()
            //    .HasMany(c => c.Contributors)
            //    .WithMany(i => i.Movies)
            //    .Map(t => t.MapLeftKey("MovieID")
            //        .MapRightKey("ContributorID")
            //        .ToTable("MovieContributor"));


        }
    }
}














//Entity States and the Attach and SaveChanges Methods

//The database context keeps track of whether entities in memory are in sync with their corresponding rows
//in the database, and this information determines what happens when you call the SaveChanges method.
//For example, when you pass a new entity to the Add method, that entity's state is set to Added.
//Then when you call the SaveChanges method, the database context issues a SQL INSERT command.

//An entity may be in one of the following states:

//    Added.The entity does not yet exist in the database.
//    The SaveChanges method must issue an INSERT statement.

//    Unchanged. Nothing needs to be done with this entity by the SaveChanges method.
//    When you read an entity from the database, the entity starts out with this status.

//    Modified. Some or all of the entity's property values have been modified.
//    The SaveChanges method must issue an UPDATE statement.

//    Deleted. The entity has been marked for deletion. The SaveChanges method must issue a DELETE statement.

//    Detached. The entity isn't being tracked by the database context.

//In a desktop application, state changes are typically set automatically.
//In a desktop type of application, you read an entity and make changes to some of its property values.
//This causes its entity state to automatically be changed to Modified. Then when you call SaveChanges,
//the Entity Framework generates a SQL UPDATE statement that updates only the actual properties
//that you changed.

//The disconnected nature of web apps doesn't allow for this continuous sequence.
//The DbContext that reads an entity is disposed after a page is rendered.
//When the HttpPost Edit action method is called, a new request is made
//and you have a new instance of the DbContext, so you have to manually set the entity state to Modified.
//Then when you call SaveChanges, the Entity Framework updates all columns of the database row,
//because the context has no way to know which properties you changed.

//If you want the SQL Update statement to update only the fields that the user actually changed,
//you can save the original values in some way (such as hidden fields)
//so that they are available when the HttpPost Edit method is called.
//Then you can create a entity using the original values,
//call the Attach method with that original version of the entity,
//update the entity's values to the new values, and then call SaveChanges.
//For more information, see Entity states and SaveChanges and Local Data.