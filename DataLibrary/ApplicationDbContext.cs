using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataLibrary.Models;
using database.Models;
using System.Reflection.Emit;

namespace DataLibrary
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Profil> Profils { get; set; }
        public DbSet<Amelioration> Ameliorations { get; set; }
        public DbSet<EquipementPossede> EquipementPossedes { get; set; }
        public DbSet<Evasion> Evasions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("YourConnectionStringHere");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profil>()
                .HasOne(p => p.Utilisateur)
                .WithMany()
                .HasForeignKey(p => p.UID);

            modelBuilder.Entity<EquipementPossede>()
                .HasKey(ep => new { ep.IDProfil, ep.NumAmelioration });

            modelBuilder.Entity<EquipementPossede>()
                .HasOne(ep => ep.Profil)
                .WithMany()
                .HasForeignKey(ep => ep.IDProfil);

            modelBuilder.Entity<EquipementPossede>()
                .HasOne(ep => ep.Amelioration)
                .WithMany()
                .HasForeignKey(ep => ep.NumAmelioration);

            modelBuilder.Entity<Evasion>()
                .HasOne(e => e.Profil)
                .WithMany()
                .HasForeignKey(e => e.IDProfil);

            modelBuilder.Entity<Evasion>()
                .HasOne(e => e.Amelioration)
                .WithMany()
                .HasForeignKey(e => e.NumAmelioration);

            modelBuilder.Entity<Evasion>()
                .Property(e => e.Score)
                .HasComputedColumnSql("NiveauEvasion * 1000 + EnnemisTues * 500 + NbBoulon * 100 + NbPiece * 10 - UtilisationPouvoirs * 250 - HPPerdus * 300");
        }
    }
}
