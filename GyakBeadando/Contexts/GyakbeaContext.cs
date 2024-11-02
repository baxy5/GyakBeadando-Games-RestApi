using System;
using System.Collections.Generic;
using GyakBeadando.Models;
using Microsoft.EntityFrameworkCore;

namespace GyakBeadando.Contexts;

public partial class GyakbeaContext : DbContext
{
    public GyakbeaContext()
    {
    }

    public GyakbeaContext(DbContextOptions<GyakbeaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Game> Games { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=123;Database=Gyakbea");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("games_pkey");

            entity.ToTable("games");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
            entity.Property(e => e.Developer)
                .HasMaxLength(40)
                .HasColumnName("developer");
            entity.Property(e => e.Isplayed)
                .HasDefaultValue(false)
                .HasColumnName("isplayed");
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .HasColumnName("name");
            entity.Property(e => e.ReleaseDate).HasColumnName("release_date");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
