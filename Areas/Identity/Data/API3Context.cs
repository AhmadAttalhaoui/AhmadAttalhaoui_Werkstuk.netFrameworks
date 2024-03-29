﻿using API3.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using API3.Models;
using Microsoft.Build.Evaluation;

namespace API3.Data;

public class API3Context : IdentityDbContext<API3User>
{
    public API3Context(DbContextOptions<API3Context> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<IdentityUserRole<Guid>>().HasKey(p => new { p.UserId, p.RoleId });
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.Entity<Critique>()
                    .HasOne(f=> f.search)
                    .WithMany(c=> c.Critiques)
                    .HasForeignKey(f => f.imdbID);

        builder.Entity<User>().HasData(

            new User {  
                Id = 1,
                UserName = "Maria",  
                Email = "Maria@gmail.com", 
                EmailConfirmed = true, 
                Achternaam = "Jaqueline", 
                Voornaam = "Maria" }
        );
    }

    public DbSet<API3.Models.User> User { get; set; }


    public DbSet<API3.Models.Search> Film { get; set; }

    public DbSet<API3.Models.ApiFilm> ApiFilm { get; set; }

    public DbSet<API3.Models.Critique> Critique { get; set; }
    

}
