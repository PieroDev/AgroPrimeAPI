using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AgroPrimeAPI.Models
{
    public partial class AgroPrimeContext : DbContext
    {
        public AgroPrimeContext()
        {
        }

        public AgroPrimeContext(DbContextOptions<AgroPrimeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Worker> Workers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(local);Database=AgroPrimeAPI;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Worker>(entity =>
            {
                entity.ToTable("Worker");
                entity.Property(e => e.NumDocumento)
                    .IsRequired();
                entity.Property(e => e.PrimerNombre)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.SegundoNombre)
                    .HasMaxLength(100);
                entity.Property(e => e.PrimerApellido)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.SegundoApellido)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.FechaNacimiento)
                    .IsRequired()
                    .HasMaxLength(100);
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        
    }
}
