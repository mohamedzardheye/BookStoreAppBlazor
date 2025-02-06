using System;
using System.Collections.Generic;
using BookStoreApp.Api.Models.booking;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace BookStoreApp.Api.Data;

public partial class BookStoreDbContext : IdentityDbContext<ApiUser>
{
 
    public BookStoreDbContext()
    {

    }
 


    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<booking_registration> booking_registration { get; set; }
    public virtual DbSet<booking_routes> booking_routes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Authors__3214EC07731B7916");

            entity.Property(e => e.Bio).HasMaxLength(250);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Books__3214EC07D76C1C79");

            entity.Property(e => e.Image).HasMaxLength(50);
            entity.Property(e => e.Isbn)
                .HasMaxLength(50)
                .HasColumnName("ISBN");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Summary).HasMaxLength(250);
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK_Books_ToTable");
        });


        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Name = "User",
                NormalizedName = "USER",
                Id = "9cac6922-ba24-400c-80ee-c4e941159a0b"

            },
              new IdentityRole
              {
                  Name = "Admin",
                  NormalizedName = "ADMIN",
                  Id = "cd2e52c4-4e26-48b6-9d23-ac77456d91f8"

              }
              );

        var hasher = new PasswordHasher<ApiUser>();

        modelBuilder.Entity<ApiUser>().HasData(
            //new ApiUser
            //{
            //    Id = "51087778-5248-4439-a7ab-1b19bf48180c",
            //    Email = "admin@bookstore.com",
            //    NormalizedEmail = "ADMIN@BOOKSTORE.COM",
            //    UserName = "admin@bookstore.com",
            //    NormalizedUserName = "ADMIN@BOOKSTORE.COM",
            //    FirstName = "System",
            //    LastName = "Admin",
            //    PasswordHash = hasher.HashPassword(null, "123456")


            //},
            //  new ApiUser
            //  {
            //      Id = "aed56a54-37fd-4a92-93c1-eb81f4d17d7d",
                  
            //      Email = "user@bookstore.com",
            //      NormalizedEmail = "USER@BOOKSTORE.COM",
            //      UserName = "user@bookstore.com",
            //      NormalizedUserName = "USER@BOOKSTORE.COM",
            //      FirstName = "System",
            //      LastName = "User",
            //      PasswordHash = hasher.HashPassword(null, "123456")

            //  }
              );


        modelBuilder.Entity<IdentityUserRole<string>>().HasData (
            //new IdentityUserRole<string>
            //{
            //    RoleId = "9cac6922-ba24-400c-80ee-c4e941159a0b",
            //    UserId = "aed56a54-37fd-4a92-93c1-eb81f4d17d7d"
            //},
            //  new IdentityUserRole<string>
            //  {
            //    RoleId = "cd2e52c4-4e26-48b6-9d23-ac77456d91f8",
            //    UserId = "51087778-5248-4439-a7ab-1b19bf48180c"
            //  }
            );

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
