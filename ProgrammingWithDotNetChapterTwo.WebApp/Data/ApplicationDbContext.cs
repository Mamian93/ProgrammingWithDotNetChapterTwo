using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProgrammingWithDotNetChapterTwo.WebApp.Models;
using ProgrammingWithDotNetChapterTwo.WebApp.Models.ModelConfiguration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrammingWithDotNetChapterTwo.WebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> AplicationUser { get; set; }
        public DbSet<Information> Information { get; set; }
        public DbSet<Bill> Bill { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
            modelBuilder.ApplyConfiguration(new BillConfiguration());
            modelBuilder.ApplyConfiguration(new InformationsConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
