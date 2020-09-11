using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Mutantes.Infraestructura.Data
{
    public partial class MutantsDbContext : DbContext
    {
        public MutantsDbContext()
        {
        }

        public MutantsDbContext(DbContextOptions<MutantsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AnalysisStats> AnalysisStats { get; set; }
        public virtual DbSet<DnaAnalyzed> DnaAnalyzed { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {

        }
          
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnalysisStats>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("analysis_stats");

                entity.Property(e => e.HumansFound).HasColumnName("humans_found");

                entity.Property(e => e.LastModification).HasColumnName("last_modification");

                entity.Property(e => e.MutantsFound).HasColumnName("mutants_found");
            });

            modelBuilder.Entity<DnaAnalyzed>(entity =>
            {
                entity.ToTable("dna_analyzed");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateAnalyzed).HasColumnName("date_analyzed");

                entity.Property(e => e.Dna)
                    .IsRequired()
                    .HasColumnName("dna");

                entity.Property(e => e.IsMutant).HasColumnName("is_mutant");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
