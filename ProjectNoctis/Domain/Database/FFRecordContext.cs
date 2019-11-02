using Microsoft.EntityFrameworkCore;
using ProjectNoctis.Domain.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Domain.Database
{
    public class FFRecordContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.;Database=ffrk;Trusted_Connection=True;");
            }
        }
        public DbSet<DbCharacters> Characters { get; set; }
        public DbSet<DbSkills> Skills { get; set; }
        public DbSet<DbEquipment> Equipment { get; set; }
        public DbSet<DbSoulbreaks> Soulbreaks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbSoulbreaks>().
                HasOne<DbCharacters>().
                WithMany(x => x.Soulbreaks).
                HasForeignKey(x => x.Character).
                HasPrincipalKey(x => x.Name);

            modelBuilder.Entity<DbCharacterEquipment>().
                HasKey(x => new { x.EquipmentId, x.CharacterId });

            modelBuilder.Entity<DbCharacterEquipment>().
                HasOne<DbEquipment>(x => x.Equipment).
                WithMany(x => x.Characters).
                HasForeignKey(x => x.EquipmentId);

            modelBuilder.Entity<DbCharacterEquipment>().
                HasOne<DbCharacters>(x => x.Character).
                WithMany(x => x.Equipment).
                HasForeignKey(x => x.CharacterId);

            modelBuilder.Entity<DbCharacterSkills>().
                HasKey(x => new { x.SkillId, x.CharacterId });

            modelBuilder.Entity<DbCharacterSkills>().
                HasOne(x => x.Character).
                WithMany(x => x.Skills).
                HasForeignKey(x => x.CharacterId);

            modelBuilder.Entity<DbCharacterSkills>().
                HasOne(x => x.Skill).
                WithMany(x => x.Characters).
                HasForeignKey(x => x.SkillId);
        }
    }
}
