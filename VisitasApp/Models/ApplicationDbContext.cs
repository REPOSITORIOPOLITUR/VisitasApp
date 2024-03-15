using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace VisitasApp.Models
{
    public partial class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Departamento> Departamentos { get; set; } = null!;
        public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; } = null!;
        public virtual DbSet<Visita> Visita { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.ToTable("Departamento");

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.IdAreaSuperior).HasColumnName("Id_AreaSuperior");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoDocumento>(entity =>
            {
                entity.ToTable("TipoDocumento");

                entity.Property(e => e.Codigo).HasMaxLength(40);

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.Nombre).HasMaxLength(60);

                entity.Property(e => e.ValorS).HasMaxLength(300);
            });

            modelBuilder.Entity<Visita>(entity =>
            {
                entity.Property(e => e.Apellidos).HasMaxLength(100);

                entity.Property(e => e.Comentario).HasMaxLength(500);

                entity.Property(e => e.Documento).HasMaxLength(50);

                entity.Property(e => e.Entrada).HasColumnType("datetime");

                entity.Property(e => e.FechaModificado).HasColumnType("datetime");

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.IdEstado).HasDefaultValueSql("((1))");

                entity.Property(e => e.IdUsuario).HasMaxLength(450);

                entity.Property(e => e.Nombres).HasMaxLength(100);

                entity.Property(e => e.Salida).HasColumnType("datetime");

                entity.HasOne(d => d.Departamento)
                    .WithMany(p => p.Visita)
                    .HasForeignKey(d => d.IdDepartamento)
                    .HasConstraintName("FK_Departamento");

                entity.HasOne(d => d.TipoDocumento)
                    .WithMany(p => p.Visita)
                    .HasForeignKey(d => d.IdTipoDocumento)
                    .HasConstraintName("FK_TipoDocumento");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
