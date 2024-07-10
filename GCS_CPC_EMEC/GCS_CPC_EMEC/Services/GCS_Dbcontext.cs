using GCS_CPC_EMEC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.Linq;

namespace GCS_CPC_EMEC.Services
{
    
   public class GCS_Dbcontext : DbContext
    {
        public DbSet<Information> Informations { get; set; } 
        public DbSet<DVI_QUANLY> Dvi_QUANLYS { get; set; }
        public DbSet<TRAM> TRAMS { get; set; }
        private readonly string _databasePath;

        public GCS_Dbcontext(string databasePath)
        {
            _databasePath = databasePath;   
           //Database.EnsureDeleted();             
           Database.EnsureCreated();           

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(string.Format("Filename={0}", _databasePath));

            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Information>().HasKey(p => new { p.SERY_CTO,p.LOAI_BCS,p.MA_GC});
            modelBuilder.Entity<TRAM>().HasKey(ba => new { ba.MA_DVIQLY, ba.MA_TRAM });
          
            
        }

       
        
    }
}
