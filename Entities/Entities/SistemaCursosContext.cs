using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Entities.Entities;

public partial class SistemaCursosContext : DbContext
{
    public SistemaCursosContext()
    {
    }

    public SistemaCursosContext(DbContextOptions<SistemaCursosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Curso> Cursos { get; set; }

    public virtual DbSet<Evaluacione> Evaluaciones { get; set; }

    public virtual DbSet<Horario> Horarios { get; set; }

    public virtual DbSet<Inscripcione> Inscripciones { get; set; }

    public virtual DbSet<Nota> Notas { get; set; }

    public virtual DbSet<Seccione> Secciones { get; set; }

    public virtual DbSet<TipoEvaluacione> TipoEvaluaciones { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //lo que añadió el profe la clase pasada
    {
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.CursoId).HasName("PK__Cursos__7E0235D75F0168BB");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Updated_at");
        });

        modelBuilder.Entity<Evaluacione>(entity =>
        {
            entity.HasKey(e => e.EvaluacionId).HasName("PK__Evaluaci__99ABA745EF6A9D60");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.Porcentaje).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Updated_at");

            entity.HasOne(d => d.Seccion).WithMany(p => p.Evaluaciones)
                .HasForeignKey(d => d.SeccionId)
                .HasConstraintName("FK__Evaluacio__Secci__534D60F1");

            entity.HasOne(d => d.TipEvaluacion).WithMany(p => p.Evaluaciones)
                .HasForeignKey(d => d.TipEvaluacionId)
                .HasConstraintName("FK__Evaluacio__TipEv__5441852A");
        });

        modelBuilder.Entity<Horario>(entity =>
        {
            entity.HasKey(e => e.HorarioId).HasName("PK__Horarios__BB881B7E31DB0584");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.Dia).HasMaxLength(20);
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Updated_at");
        });

        modelBuilder.Entity<Inscripcione>(entity =>
        {
            entity.HasKey(e => e.InscripcionId).HasName("PK__Inscripc__168316B926F2EA92");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Updated_at");

            entity.HasOne(d => d.Seccion).WithMany(p => p.Inscripciones)
                .HasForeignKey(d => d.SeccionId)
                .HasConstraintName("FK__Inscripci__Secci__4CA06362");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Inscripciones)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Inscripci__Usuar__4BAC3F29");
        });

        modelBuilder.Entity<Nota>(entity =>
        {
            entity.HasKey(e => e.NotaId).HasName("PK__Notas__EF36CC1A52CBAF08");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.Total).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Updated_at");

            entity.HasOne(d => d.Evaluacion).WithMany(p => p.Nota)
                .HasForeignKey(d => d.EvaluacionId)
                .HasConstraintName("FK__Notas__Evaluacio__5812160E");

            entity.HasOne(d => d.Inscripcion).WithMany(p => p.Nota)
                .HasForeignKey(d => d.InscripcionId)
                .HasConstraintName("FK__Notas__Inscripci__59063A47");
        });

        modelBuilder.Entity<Seccione>(entity =>
        {
            entity.HasKey(e => e.SeccionId).HasName("PK__Seccione__18B61641F6B0AA36");

            entity.Property(e => e.Carrera)
                .HasMaxLength(100)
                .HasDefaultValue("Todas");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.CuposMax).HasDefaultValue(20);
            entity.Property(e => e.Grupo).HasMaxLength(20);
            entity.Property(e => e.Periodo).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Updated_at");

            entity.HasOne(d => d.Curso).WithMany(p => p.Secciones)
                .HasForeignKey(d => d.CursoId)
                .HasConstraintName("FK__Secciones__Curso__44FF419A");

            entity.HasOne(d => d.Horario).WithMany(p => p.Secciones)
                .HasForeignKey(d => d.HorarioId)
                .HasConstraintName("FK__Secciones__Horar__45F365D3");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Secciones)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Secciones__Usuar__440B1D61");
        });

        modelBuilder.Entity<TipoEvaluacione>(entity =>
        {
            entity.HasKey(e => e.TipEvaluacionId).HasName("PK__TipoEval__16F406B22731C6E4");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Updated_at");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE7B8BFE195CE");

            entity.HasIndex(e => e.Correo, "UQ__Usuarios__60695A190452987C").IsUnique();

            entity.HasIndex(e => e.Identificacion, "UQ__Usuarios__D6F931E54F1B329A").IsUnique();

            entity.Property(e => e.Apellido1).HasMaxLength(100);
            entity.Property(e => e.Apellido2).HasMaxLength(100);
            entity.Property(e => e.Carrera).HasMaxLength(100);
            entity.Property(e => e.Contrasena).HasMaxLength(100);
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.Identificacion).HasMaxLength(50);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Rol).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Updated_at");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
