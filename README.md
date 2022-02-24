# ASP.NET-MVC-App-with-EF6

  ## Adam's Media Database
   -demo: https://adams-media-database-app.azurewebsites.net
 
  ## Description
  Welcome to my personal media database! I have built this ASP.NET MVC app with Entity Framework 6. I started this project so I could gain a deeper understanding of MVC design patterns 
  and learn how to interact with databases through Entity Framework. 
  
  ## Current Features
  * Seed Method which populates new database with a sample set of data to demonstrate app's key features
  * CRUD functionality:
     * allows users to add, read, edit, and remove authors, books, movies, and contributors(cast and crew) from database
  * Type-Per-Concrete-Class(TPC) Entity Inheritance patterns:
     * book and movie models inherit from abstract medium base class model
     * author and contributor models inherit from abstract person base class model
  * Multiple Entity Relationships:
     * one-to-many relationship between author and books
        ** uses foreign key and navigation properties
     * many-to-many relationship between contributors(cast and crew) and movies 
        ** uses navigation properties and a join table with payload(roles)
  * Utilizes both Models and ViewModels to allow users to:
     * display data from multiple tables in one page
     * update multiple db tables in one submission

## Intended Improvements
Time permitting, I would love to add a new join table to store which movies are adapted from existing books and books which are in turn novelizations of existing movies. Beyond that, I would really enjoy incorporating TV shows and Radio programs with their own respective join tables which would indicate which were adapted from existing intellectual properties--we haven't even got to comic books yet! 
