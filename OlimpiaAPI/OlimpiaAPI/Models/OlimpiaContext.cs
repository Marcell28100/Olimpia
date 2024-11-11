using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OlimpiaAPI.Models;

public partial class OlimpiaContext : DbContext
{
    public OlimpiaContext()
    {
    }

    public OlimpiaContext(DbContextOptions<OlimpiaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Datum> Data { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;database=olimpia;user=root;password=;sslmode=none;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Datum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("data");

            entity.HasIndex(e => e.PlayerId, "PlayerId");

            entity.Property(e => e.Country)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("tinytext");
            entity.Property(e => e.County)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("text");
            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("text");
            entity.Property(e => e.PlayerId).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UpdatedTime)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Player).WithMany(p => p.Data)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("data_ibfk_1");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("players");

            entity.Property(e => e.Age)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("tinyint(4)");
            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.Height)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.Name)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("tinytext");
            entity.Property(e => e.Weight)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
