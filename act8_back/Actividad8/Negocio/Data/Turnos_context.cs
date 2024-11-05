#nullable disable
using System;
using System.Collections.Generic;
using Actividad8.Negocio.Data;
using Microsoft.EntityFrameworkCore;

namespace Negocio.Data.Entities;

public partial class TurnosDbContext : DbContext
{
    public TurnosDbContext(DbContextOptions<TurnosDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TDetallesTurno> TDetallesTurnos { get; set; }

    public virtual DbSet<TServicio> TServicios { get; set; }

    public virtual DbSet<TTurno> TTurnos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TDetallesTurno>(entity =>
        {
            entity.HasKey(e => new { e.IdTurno, e.IdServicio });

            entity.ToTable("T_DETALLES_TURNO");

            entity.Property(e => e.IdTurno).HasColumnName("id_turno");
            entity.Property(e => e.IdServicio).HasColumnName("id_servicio");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("observaciones");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.TDetallesTurnos)
                .HasForeignKey(d => d.IdServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_det_turno_servicios");

            entity.HasOne(d => d.IdTurnoNavigation).WithMany(p => p.TDetallesTurnos)
                .HasForeignKey(d => d.IdTurno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_det_turno_turnos");
            entity.Navigation(e => e.IdTurnoNavigation).AutoInclude();
            //entity.Navigation(e => e.IdServicioNavigation).AutoInclude();
        });
        modelBuilder.Entity<TServicio>(entity =>
        {
            entity.ToTable("T_SERVICIOS");

            entity.Property(e => e.IdServicio)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Costo).HasColumnName("costo");
            entity.Property(e => e.EnPromocion)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("enPromocion");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Navigation(e => e.TDetallesTurnos).AutoInclude();
        });

        modelBuilder.Entity<TTurno>(entity =>
        {
            entity.ToTable("T_TURNOS");

            entity.Property(e => e.IdTurno).HasColumnName("id");
            entity.Property(e => e.Cliente)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cliente");
            entity.Property(e => e.Fecha)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("fecha");
            entity.Property(e => e.FechaCancelacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_cancelacion");
            entity.Property(e => e.Hora)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("hora");
            entity.Property(e => e.MotivoCancelacion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("motivo_cancelacion");
            entity.Navigation(e => e.TDetallesTurnos).AutoInclude();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}