using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAL
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationDbContext:IdentityDbContext<ApplicationUsers,ApplicationRoles,string>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option):base(option)
        {

        }
        public DbSet<Country> Countries { get; set; }

        public DbSet<EmigrationType> EmigrationTypes { get; set; }

        public DbSet<EmigrateCountry> EmigrateCountries { get; set; }

        public DbSet<CountryCoverImage> CountryCoverImages { get; set; }

        public DbSet<CountryCoverVideo> CountryCoverVideos { get; set; }
    }
}
