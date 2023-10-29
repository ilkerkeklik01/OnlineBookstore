using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration):base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Book Entity
            modelBuilder.Entity<Book>(book =>
            {
                book.ToTable("Books").HasKey(x => x.Id);
                book.Property(x=>x.Id).UseIdentityColumn(1,1);
                book.Property(p => p.Id).HasColumnName("Id");
                book.Property(p => p.Title).HasColumnName("Title");
                book.Property(p => p.Description).HasColumnName("Description").IsRequired(false);
                book.Property(p => p.Price).HasColumnName("Price");
                book.Property(p => p.Author).HasColumnName("Author");
                book.Property(p => p.CategoryId).HasColumnName("CategoryId");
                book.Property(p => p.BookshelfId).HasColumnName("BookshelfId").IsRequired(false);
                book.Property(p => p.CoverImagePath).HasColumnName("CoverImagePath").IsRequired(false);
                book.Property(p => p.PublicationDate).HasColumnName("PublicationDate");
                //book.Property(p => p.OrderItemId).HasColumnName("OrderItemId").IsRequired(false);
                book.Property(p => p.Discount).HasColumnName("Discount");

            });

            modelBuilder.Entity<Book>(book =>
            {
                book.HasOne(x => x.Category).WithMany(x=>x.Books).
                    OnDelete(DeleteBehavior.Restrict);
                book.HasOne(x => x.Bookshelf).WithMany(x=>x.Books);
                book.HasMany(x => x.Reviews);
                book.HasMany(x => x.OrderItems);

            });

            

            //Bookshelf Entity
            modelBuilder.Entity<Bookshelf>(bookshelf =>
            {
                bookshelf.ToTable("Bookshelves").HasKey(x => x.Id);
                bookshelf.Property(x => x.Id).UseIdentityColumn(1, 1);
                bookshelf.Property(p => p.Id).HasColumnName("Id");
                bookshelf.Property(p => p.Name).HasColumnName("Name");
                bookshelf.Property(p => p.UserId).HasColumnName("UserId");
            });
            modelBuilder.Entity<Bookshelf>(bookshelf =>
            {
                bookshelf.HasOne(x => x.User).WithMany(x=>x.Bookshelves).
                    OnDelete(DeleteBehavior.Restrict);
                bookshelf.HasMany(x => x.Books);
            });

            //CategoryEntity 
            modelBuilder.Entity<Category>(category =>
            {
                category.ToTable("Categories").HasKey(x => x.Id);
                category.Property(x=>x.Id).UseIdentityColumn(1,1);
                category.Property(p => p.Id).HasColumnName("Id");
                category.Property(p => p.Name).HasColumnName("Name");
            });
            modelBuilder.Entity<Category>(category =>
            {
                category.HasMany(x => x.Books);
            });

            //Order Entity 


            modelBuilder.Entity<Order>(order => {
                order.ToTable("Orders").HasKey(x => x.Id);
                order.Property(x=>x.Id).UseIdentityColumn(1,1);
                order.Property(p => p.Id).HasColumnName("Id");
                order.Property(p => p.UserId).HasColumnName("UserId");
                order.Property(p => p.TotalPrice).HasColumnName("TotalPrice");
                order.Property(p => p.OrderDate).HasColumnName("OrderDate");
            });
            modelBuilder.Entity<Order>(order =>
            {
                order.HasMany(x => x.OrderItems);
                order.HasOne(x => x.User).WithMany(x=>x.Orders).
                    OnDelete(DeleteBehavior.Restrict);
            });

            //OrderItem Entity

        //    public string BookTitleAtThatTime { get; set; }
        //public string BookAuthorAtThatTime { get; set; }
        //public int BookCategoryIdAtThatTime { get; set; }
        //public string? BookDescriptionAtThatTime { get; set; }
        //public decimal BookPriceAtThatTime { get; set; }
        //public decimal BookDiscountAtThatTime { get; set; }
        //public DateTime BookPublicationDateAtThatTime { get; set; }
        //public string? BookCoverImagePathAtThatTime { get; set; }



        modelBuilder.Entity<OrderItem>(orderItem =>
            {
                orderItem.ToTable("OrderItems").HasKey(x => x.Id);
                orderItem.Property(x=>x.Id).UseIdentityColumn(1,1);
                orderItem.Property(p => p.Id).HasColumnName("Id");
                orderItem.Property(p => p.OrderId).HasColumnName("OrderId").IsRequired(false);
                orderItem.Property(p => p.BookId).HasColumnName("BookId");
                orderItem.Property(p => p.Quantity).HasColumnName("Quantity");

                orderItem.Property(p => p.BookTitleAtThatTime).HasColumnName("BookTitleAtThatTime");
                orderItem.Property(p => p.BookAuthorAtThatTime).HasColumnName("BookAuthorAtThatTime");
                orderItem.Property(p => p.BookCategoryIdAtThatTime).HasColumnName("BookCategoryIdAtThatTime");
                orderItem.Property(p => p.BookDescriptionAtThatTime).HasColumnName("BookDescriptionAtThatTime");
                orderItem.Property(p => p.BookPriceAtThatTime).HasColumnName("BookPriceAtThatTime");
                orderItem.Property(p => p.BookDiscountAtThatTime).HasColumnName("BookDiscountAtThatTime");
                orderItem.Property(p => p.BookPublicationDateAtThatTime).HasColumnName("BookPublicationDateAtThatTime");
                orderItem.Property(p => p.BookCoverImagePathAtThatTime).HasColumnName("BookCoverImagePathAtThatTime");


            });
            modelBuilder.Entity<OrderItem>(orderItem =>
            {
                orderItem.HasOne(x => x.Book).
                    WithMany(x=>x.OrderItems).HasForeignKey(x=>x.BookId).OnDelete(DeleteBehavior.Restrict);
                orderItem.HasOne(x => x.Order);
            });

            //Review Entity
           

            modelBuilder.Entity<Review>(review => 
            {
                review.ToTable("Reviews").HasKey(x => x.Id);
                review.Property(x=>x.Id).UseIdentityColumn(1,1);
                review.Property(p=>p.Id).HasColumnName("Id");
                review.Property(p=>p.UserId).HasColumnName("UserId");
                review.Property(p => p.BookId).HasColumnName("BookId");
                review.Property(p => p.ReviewText).HasColumnName("ReviewText");
                review.Property(p => p.Rating).HasColumnName("Rating").IsRequired(false);
            });
            modelBuilder.Entity<Review>(review =>
            {
                review.HasOne(x => x.Book);
                review.HasOne(x => x.User).WithMany(x=>x.Reviews).
                    OnDelete(DeleteBehavior.Restrict);
            });

            //User Entity
            

            modelBuilder.Entity<User>(user => 
            {
                user.ToTable("Users").HasKey(x=>x.Id);
                
                user.Property(x=>x.Id).UseIdentityColumn(1,1);
                user.Property(p=>p.Id).HasColumnName("Id").IsRequired(true);
                user.Property(p=>p.Username).HasColumnName("Username").IsRequired(true); 
                user.Property(p=>p.Email).HasColumnName("Email").IsRequired(true);
                user.Property(p=>p.Password).HasColumnName("Password").IsRequired(true);
                user.Property(p=>p.RegistrationDate).HasColumnName("RegistrationDate").IsRequired(true);
                user.Property(p=>p.PasswordUpdatedAt).HasColumnName("PasswordUpdatedAt").IsRequired(true);

            });
            modelBuilder.Entity<User>(user =>
            {
                user.HasMany(x => x.Bookshelves);
                user.HasMany(x => x.Orders);
                user.HasMany(x => x.Reviews);
            });
            //Seeds
            //Book[] bookEntitySeeds = {
            //    new Book { Id = 1,Author="Charles Dickens",CategoryId=1,Title="Buyuk Umutlar",Price=50,PublicationDate=DateTime.Now},
            //    new Book { Id = 2,Author="Sabahattin Ali",CategoryId=2,Title="Kuyucakli Yusuf",Price=100,PublicationDate=DateTime.Now},
            //};
            //modelBuilder.Entity<Book>().HasData(bookEntitySeeds);



        }//OnModelCreating end
    }
}
