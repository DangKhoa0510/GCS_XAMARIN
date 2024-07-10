using GCS_CPC_EMEC.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace GCS_CPC_EMEC.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Information> Informations { get; set; } 

        private readonly string _databasePath;

        public DatabaseContext(string databasePath)
        {
            _databasePath = databasePath;
          
            Database.EnsureCreated();
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(string.Format("Filename={0}", _databasePath));
        }
    }
}
