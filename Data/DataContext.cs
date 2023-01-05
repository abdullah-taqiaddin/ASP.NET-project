using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Final_Project.Models;

namespace Final_Project.Data
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Final_Project.Models.Book> Book { get; set; }

        public DbSet<Final_Project.Models.Author> Author { get; set; }

        public DbSet<Final_Project.Models.Authorship> Authorship { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
               new Book { BookId = 1, Name = "Harry Potter", Catagory = eCatagory.Fantasy, Price = 50.0, Cover = "/images/Harry_Potter_and_the_Philosopher's_Stone.jpg" , Publsiher ="PublisherCo" , ReleseDate = new DateTime(1995,5,2)},
               new Book { BookId = 2, Name = "Spiderman: Kravens Last Hunt", Catagory = eCatagory.Action, Price = 65.99, Cover = "/images/SpidermanKLH.jpg", Publsiher = "NewPublisherCo", ReleseDate = new DateTime(2002, 5, 2) },
               new Book { BookId = 3, Name = "Cloud Atlas", Catagory = eCatagory.ScienceFiction, Price = 35.0, Cover = "/images/Cloud_atlas.jpg", Publsiher = "ThePublisher", ReleseDate = new DateTime(1951, 2, 2) }
               );
            modelBuilder.Entity<Author>().HasData(
                    new Author { AuthorId = 1, Name = "JK Rowlings", Phone = 028653212, Email = "JK@gmail.com", DOB = new DateTime(1991, 12, 1), },
                    new Author { AuthorId = 2, Name = "J. M. DeMatteis", Phone = 088653212, Email = "JMD@outlook.com", DOB = new DateTime(1985, 11, 1), },
                    new Author { AuthorId = 3, Name = "David Mitchell", Phone = 098653212, Email = "DM@gmail.com", DOB = new DateTime(1963, 10, 5),}
                );
            modelBuilder.Entity<Authorship>().HasData(
                new Authorship
                {
                    
                    ID = 1,
                    BookId = 1,
                    AuthorId = 1,
                    Role = eRole.Author
                },
                new Authorship
                {
                    ID = 2,
                    BookId = 1,
                    AuthorId = 2,
                    Role = eRole.CoAuthor
                }, new Authorship
                {
                    ID = 3,
                    BookId = 2,
                    AuthorId = 3,
                    Role = eRole.Author
                },
                new Authorship
                {
                    ID = 4,
                    BookId = 2,
                    AuthorId = 3,
                    Role = eRole.CoAuthor
                }
                );
        }
    }
}
