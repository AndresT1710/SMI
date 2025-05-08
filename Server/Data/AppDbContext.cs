using Microsoft.EntityFrameworkCore;
using SMI.Shared.Models;

namespace SMI.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Persona> Persona { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Profesion> Profesion { get; set; }
        public DbSet<PersonaProfesion> PersonaProfesion { get; set; }
        public DbSet<TipoDocumento> TipoDocumento { get; set; }
        public DbSet<PersonaDocumento> PersonaDocumento { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>(entity =>
            {
                entity.ToTable("Persona");
                entity.HasKey(e => e.id);
                entity.Property(e => e.nombre).HasMaxLength(100);
                entity.Property(e => e.apellido).HasMaxLength(100);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Correo).HasMaxLength(200);
                entity.Property(e => e.Clave).HasMaxLength(200);

                //Tokens
                entity.Property(e => e.RefreshToken).HasMaxLength(500);
                entity.Property(e => e.RefreshTokenExpiryTime);
                entity.HasOne(u => u.Persona)
                      .WithMany(p => p.Usuarios)
                      .HasForeignKey(u => u.Id_Persona)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Profesion>(entity =>
            {
                entity.ToTable("Profesion");
                entity.HasKey(e => e.id);
                entity.Property(e => e.nombre).HasMaxLength(100);
            });

            modelBuilder.Entity<PersonaProfesion>(entity =>
            {
                entity.ToTable("PersonaProfesion");
                entity.HasNoKey();

                entity.HasOne(pp => pp.Persona)
                      .WithMany()
                      .HasForeignKey(pp => pp.id_Persona)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(pp => pp.Profesion)
                      .WithMany()
                      .HasForeignKey(pp => pp.id_Profesion)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PersonaDocumento>()
            .HasKey(pd => new { pd.id_Persona, pd.id_TipoDocumento }); // Asegúrate de que la clave primaria esté configurada correctamente.

            modelBuilder.Entity<PersonaDocumento>()
                .HasOne(pd => pd.Persona)
                .WithMany(p => p.PersonaDocumentos)
                .HasForeignKey(pd => pd.id_Persona);

            modelBuilder.Entity<PersonaDocumento>()
                .HasOne(pd => pd.TipoDocumento)
                .WithMany(td => td.PersonaDocumentos)
                .HasForeignKey(pd => pd.id_TipoDocumento);


            base.OnModelCreating(modelBuilder);
        }
    }
}
