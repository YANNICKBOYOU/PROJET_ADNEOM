using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Adneom.CoffeMachine.Domain.Entities
{
    public partial class DbEntityContext : DbContext
    {
        public DbEntityContext()
        {
        }

        public DbEntityContext(DbContextOptions<DbEntityContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Machine> Machine { get; set; }
        public virtual DbSet<OperateurService> OperateurService { get; set; }
        public virtual DbSet<ServiceMachine> ServiceMachine { get; set; }
        public virtual DbSet<TypeBoisson> TypeBoisson { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=HBOYOU\\DEVSERVER2012;Database=MachineDB;Persist Security Info=True;User ID=sa;Password=P@ssw0rd;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<Machine>(entity =>
            {
                entity.ToTable("machine");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(60);
            });

            modelBuilder.Entity<OperateurService>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Operateur)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<ServiceMachine>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AvecMug).HasDefaultValueSql("((0))");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.OperateurId).HasColumnName("OperateurID");

                entity.Property(e => e.QuantiteSucre).HasDefaultValueSql("((0))");

                entity.Property(e => e.TypeBoissonId).HasColumnName("TypeBoissonID");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.ServiceMachine)
                    .HasForeignKey(d => d.MachineId)
                    .HasConstraintName("FK_ServiceMachine_machine");

                entity.HasOne(d => d.Operateur)
                    .WithMany(p => p.ServiceMachine)
                    .HasForeignKey(d => d.OperateurId)
                    .HasConstraintName("FK_ServiceMachine_OperateurService");

                entity.HasOne(d => d.TypeBoisson)
                    .WithMany(p => p.ServiceMachine)
                    .HasForeignKey(d => d.TypeBoissonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceMachine_TypeBoisson");
            });

            modelBuilder.Entity<TypeBoisson>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.TypeBoisson1)
                    .IsRequired()
                    .HasColumnName("TypeBoisson")
                    .HasMaxLength(64);
            });
        }
    }
}
