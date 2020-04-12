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
                optionsBuilder.UseSqlServer(@"Server=tcp:ffrkapi.database.windows.net,1433;Initial Catalog=ffrk;Persist Security Info=False;User ID=purgedmoon;Password=@Pa33word;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=3000;");
            }
        }
        public DbSet<DbCharacters> Characters { get; set; }
        public DbSet<DbSoulbreaks> Soulbreaks { get; set; }
        public DbSet<DbSynchroCommands> SynchroCommands { get; set; }
        public DbSet<DbBurstCommands> BurstCommands { get; set; }
        public DbSet<DbBraveCommands> BraveCommands { get; set; }
        public DbSet<DbMagicite> Magicites { get; set; }
        public DbSet<DbStatuses> Statuses { get; set; }
        public DbSet<DbOther> Others { get; set; }
        public DbSet<DbLegendSpheres> LegendSpheres { get; set; }
        public DbSet<DbRecordSpheres> RecordSpheres { get; set; }
        public DbSet<DbRecordBoards> RecordBoards { get; set; }
        public DbSet<DbLegendMaterias> LegendMaterias { get; set; }
        public DbSet<DbRecordMaterias> RecordMaterias { get; set; }
        public DbSet<DbEquipment> Equipment { get; set; }
        public DbSet<DbSkills> Skills { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbSoulbreaks>().
                HasOne<DbCharacters>().
                WithMany(x => x.Soulbreaks).
                HasForeignKey(x => x.Character).
                HasPrincipalKey(x => x.Name);

            modelBuilder.Entity<DbRecordMaterias>().
                HasOne<DbCharacters>().
                WithMany(x => x.RecordMaterias).
                HasForeignKey(x => x.Character).
                HasPrincipalKey(x => x.Name);

            modelBuilder.Entity<DbLegendMaterias>().
                HasOne<DbCharacters>().
                WithMany(x => x.LegendMaterias).
                HasForeignKey(x => x.Character).
                HasPrincipalKey(x => x.Name);

            modelBuilder.Entity<DbLegendSpheres>().
                HasOne<DbCharacters>().
                WithOne(x => x.LegendSphere).
                HasForeignKey<DbLegendSpheres>(x => x.Character).
                HasPrincipalKey<DbCharacters>(x => x.Name);

            modelBuilder.Entity<DbRecordSpheres>().
                HasOne<DbCharacters>().
                WithOne(x => x.RecordSphere).
                HasForeignKey<DbRecordSpheres>(x => x.Character).
                HasPrincipalKey<DbCharacters>(x => x.Name);

            modelBuilder.Entity<DbRecordBoards>().
                HasOne<DbCharacters>().
                WithOne(x => x.RecordBoard).
                HasForeignKey<DbRecordBoards>(x => x.Character).
                HasPrincipalKey<DbCharacters>(x => x.Name);

            modelBuilder.Entity<DbCharacterEquipment>().
                HasKey(x => new { x.EquipmentId, x.CharacterId });

            modelBuilder.Entity<DbCharacterEquipment>().
                HasOne<DbEquipment>(x => x.Equipment).
                WithMany().
                HasForeignKey(x => x.EquipmentId);

            modelBuilder.Entity<DbCharacterEquipment>().
                HasOne<DbCharacters>(x => x.Character).
                WithMany(x => x.CharacterEquipment).
                HasForeignKey(x => x.CharacterId);

            modelBuilder.Entity<DbCharacterSkills>().
                HasKey(x => new { x.SkillId, x.CharacterId });

            modelBuilder.Entity<DbCharacterSkills>().
                HasOne(x => x.Character).
                WithMany(x => x.CharacterSkills).
                HasForeignKey(x => x.CharacterId);

            modelBuilder.Entity<DbCharacterSkills>().
                HasOne(x => x.Skill).
                WithMany(x => x.CharacterSkills).
                HasForeignKey(x => x.SkillId);

            modelBuilder.Entity<DbMagicite>().
                HasMany(x => x.Passives);

            modelBuilder.Entity<DbSynchroCommands>().
                HasOne<DbSoulbreaks>().
                WithMany(x => x.SynchroCommands).
                HasForeignKey(x => new { x.Source, x.Character }).
                HasPrincipalKey(x => new { x.Name, x.Character });

            modelBuilder.Entity<DbBurstCommands>().
                HasOne<DbSoulbreaks>().
                WithMany(x => x.BurstCommands).
                HasForeignKey(x => new { x.Source, x.Character }).
                HasPrincipalKey(x => new { x.Name, x.Character });

            modelBuilder.Entity<DbBraveCommands>().
                HasOne<DbSoulbreaks>().
                WithMany(x => x.BraveCommands).
                HasForeignKey(x => new { x.Source, x.Character}).
                HasPrincipalKey(x => new { x.Name, x.Character});

            modelBuilder.Entity<DbSoulbreakStatuses>().
                HasKey(x => new { x.SoulbreakId, x.StatusId });

            modelBuilder.Entity<DbSoulbreakStatuses>().
                HasOne(x => x.Soulbreak).
                WithMany(x => x.SoulbreakStatuses).
                HasForeignKey(x => x.SoulbreakId);

            modelBuilder.Entity<DbSoulbreakStatuses>().
                HasOne(x => x.Status).
                WithMany(x => x.SoulbreakStatuses).
                HasForeignKey(x => x.StatusId);

        }
    }
}
