using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Bookshelf> Bookshelves { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Order> ORders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }


        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //    base.OnConfiguring(
            //        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SomeConnectionString")));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Book Entity
            modelBuilder.Entity<Book>(book =>
            {
                book.ToTable("Books").HasKey(x => x.Id);
                book.Property(p => p.Id).HasColumnName("Id");
                book.Property(p => p.Title).HasColumnName("Title");
                book.Property(p => p.Description).HasColumnName("Description");
                book.Property(p => p.Price).HasColumnName("Price");
                book.Property(p => p.Author).HasColumnName("Author");
                book.Property(p => p.CategoryId).HasColumnName("CategoryId");
                book.Property(p => p.CoverImagePath).HasColumnName("CoverImagePath");
                book.Property(p => p.PublicationDate).HasColumnName("PublicationDate");
            });

            Book[] bookEntitySeeds = {
            new Book { Id = 1,Author="Charles Dickens",CategoryId=1,Title="Buyuk Umutlar",Price=50,PublicationDate=DateTime.Now},
            new Book { Id = 2,Author="Sabahattin Ali",CategoryId=2,Title="Kuyucakli Yusuf",Price=100,PublicationDate=DateTime.Now},
            };
            modelBuilder.Entity<Book>().HasData(bookEntitySeeds);

            //Bookshelf Entity
            modelBuilder.Entity<Bookshelf>(bookshelf =>
            {
                bookshelf.ToTable("Bookshelves").HasKey(x => x.Id);
                bookshelf.Property(p => p.Id).HasColumnName("Id");
                bookshelf.Property(p => p.Name).HasColumnName("Name");
                bookshelf.Property(p => p.UserId).HasColumnName("UserId");
            });

            //CategoryEntity 
            modelBuilder.Entity<Category>(category =>
            {
                category.ToTable("Categories").HasKey(x => x.Id);
                category.Property(p => p.Id).HasColumnName("Id");
                category.Property(p => p.Name).HasColumnName("Name");
            });

            //Order Entity 


            modelBuilder.Entity<Order>(order => {
                order.ToTable("Orders").HasKey(x => x.Id);
                order.Property(p => p.Id).HasColumnName("Id");
                order.Property(p => p.UserId).HasColumnName("UserId");
                order.Property(p => p.TotalPrice).HasColumnName("TotalPrice");
                order.Property(p => p.OrderDate).HasColumnName("OrderDate");
            });

            //OrderItem Entity

            modelBuilder.Entity<OrderItem>(orderItem =>
            {
                orderItem.ToTable("OrderItems").HasKey(x => x.Id);
                orderItem.Property(p => p.Id).HasColumnName("Id");
                orderItem.Property(p => p.OrderId).HasColumnName("OrderId");
                orderItem.Property(p => p.BookId).HasColumnName("BookId");
                orderItem.Property(p => p.Quantity).HasColumnName("Quantity");
                orderItem.Property(p => p.Discount).HasColumnName("Discount");

            });

            //Review Entity
           

            modelBuilder.Entity<Review>(review => 
            {
                review.ToTable("Reviews").HasKey(review => review.Id);
                review.Property(p=>p.Id).HasColumnName("Id");
                review.Property(p=>p.UserId).HasColumnName("UserId");
                review.Property(p => p.BookId).HasColumnName("BookId");
                review.Property(p => p.ReviewText).HasColumnName("ReviewText");
                review.Property(p => p.Rating).HasColumnName("Rating");

            });

            //User Entity
            

            modelBuilder.Entity<User>(user => 
            {
                user.ToTable("Users").HasKey(x=>x.Id);
                user.Property(p=>p.Id).HasColumnName("Id");
                user.Property(p=>p.Username).HasColumnName("Username");
                user.Property(p=>p.Email).HasColumnName("Email");
                user.Property(p=>p.Password).HasColumnName("Password");
                user.Property(p=>p.RegistrationDate).HasColumnName("RegistrationDate");
                user.Property(p=>p.PasswordUpdatedAt).HasColumnName("PasswordUpdatedAt");

            });


            //Bookshelf Book many to many junction not implemented yet


        }//OnModelCreating end
    }
}
