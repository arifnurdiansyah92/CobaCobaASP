using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CobaCoba.Models;

namespace CobaCoba.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<CobaCoba.Models.Buku> Buku { get; set; }

        public DbSet<CobaCoba.Models.Kelas> Kelas { get; set; }

        public DbSet<CobaCoba.Models.Siswa> Siswa { get; set; }

        public DbSet<CobaCoba.Models.User> User { get; set; }

        public DbSet<CobaCoba.Models.Food> Food { get; set; }

        public DbSet<CobaCoba.Models.SalesOrderHeader> SalesOrderHeader { get; set; }

        public DbSet<CobaCoba.Models.SalesOrderLine> SalesOrderLine { get; set; }


        public DbSet<CobaCoba.Models.ApplicationUser> ApplicationUser { get; set; }



    }
}
