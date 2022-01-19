using ExcellentTasteCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExcellentTasteCore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bestelling> Bestelling { get; set; }
        public virtual DbSet<Consumptie> Consumpties { get; set; }
        public virtual DbSet<ConsumptieGroep> ConsumptieGroeps { get; set; }
        public virtual DbSet<ConsumptieItem> ConsumptieItems { get; set; }
        public virtual DbSet<Klant> Klant { get; set; }
        public virtual DbSet<Reservering> Reservering { get; set; }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //                optionsBuilder.UseSqlServer("Server=.;Database=ExcellentTasteDbCore;Trusted_Connection=True;");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bestelling>(entity =>
            {
                entity.ToTable("Bestelling");

                entity.Property(e => e.BestellingId).HasColumnName("BestellingId");

                entity.Property(e => e.Aantal).HasColumnName("Aantal");

                entity.Property(e => e.ConsumptieItemCode)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("ConsumptieItemCode");

                entity.Property(e => e.DateTimeBereidingConsumptie)
                    .HasColumnType("datetime")
                    .HasColumnName("DateTimeBereidingConsumptie");

                entity.Property(e => e.Prijs)
                    .HasColumnType("decimal(19, 4)")
                    .HasColumnName("Prijs");

                entity.Property(e => e.ReserveringId).HasColumnName("ReserveringId");

                entity.Property(e => e.Totaal)
                    .HasColumnType("decimal(19, 4)")
                    .HasColumnName("Totaal");

                entity.HasOne(d => d.ConsumptieItem)
                    .WithMany(p => p.Bestelling)
                    .HasForeignKey(d => d.ConsumptieItemCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bestelling_ConsumptieItem");

                entity.HasOne(d => d.Reservering)
                    .WithMany(p => p.Bestelling)
                    .HasForeignKey(d => d.ReserveringId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bestelling_Reservering");
            });

            modelBuilder.Entity<Consumptie>(entity =>
            {
                entity.HasKey(e => e.ConsumptieCode);

                entity.ToTable("Consumptie");

                entity.Property(e => e.ConsumptieCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("ConsumptieCode");

                entity.Property(e => e.ConsumptieNaam)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ConsumptieNaam");
            });

            modelBuilder.Entity<ConsumptieGroep>(entity =>
            {
                entity.HasKey(e => e.ConsumptieGroepCode);

                entity.ToTable("ConsumptieGroep");

                entity.Property(e => e.ConsumptieGroepCode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("ConsumptieGroepCode");

                entity.Property(e => e.ConsumptieCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("ConsumptieCode");

                entity.Property(e => e.ConsumptieGroepNaam)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("ConsumptieGroepNaam");

                entity.HasOne(d => d.Consumptie)
                    .WithMany(p => p.ConsumptieGroep)
                    .HasForeignKey(d => d.ConsumptieCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubConsumptie_Consumptie");
            });

            modelBuilder.Entity<ConsumptieItem>(entity =>
            {
                entity.HasKey(e => e.ConsumptieItemCode);

                entity.ToTable("ConsumptieItem");

                entity.Property(e => e.ConsumptieItemCode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("ConsumptieItemCode");

                entity.Property(e => e.ConsumptieGroepCode)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("ConsumptieGroepCode");

                entity.Property(e => e.ConsumptieItemNaam)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("ConsumptieItemNaam");

                entity.Property(e => e.Prijs)
                    .HasColumnType("decimal(19, 4)")
                    .HasColumnName("Prijs");

                entity.HasOne(d => d.ConsumptieGroep)
                    .WithMany(p => p.ConsumptieItems)
                    .HasForeignKey(d => d.ConsumptieGroepCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ConsumptieItem_SubConsumptie");
            });

            modelBuilder.Entity<Klant>(entity =>
            {
                entity.ToTable("Klant");

                entity.Property(e => e.KlantId).HasColumnName("KlantId");

                entity.Property(e => e.KlantNaam)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("KlantNaam");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Telefoon)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("Telefoon");
            });

            modelBuilder.Entity<Reservering>(entity =>
            {
                entity.ToTable("Reservering");

                entity.Property(e => e.ReserveringId).HasColumnName("ReserveringId");

                entity.Property(e => e.AantalPersonen).HasColumnName("AantalPersonen");

                entity.Property(e => e.Betalingswijze)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("Betalingswijze");

                entity.Property(e => e.BonDatum)
                    .HasColumnType("datetime")
                    .HasColumnName("BonDatum");

                entity.Property(e => e.BonTotaal)
                    .HasColumnType("decimal(19, 4)")
                    .HasColumnName("BonTotaal");

                entity.Property(e => e.Datum)
                    .HasColumnType("datetime")
                    .HasColumnName("Datum");

                entity.Property(e => e.DatumToegevoegd)
                    .HasColumnType("datetime")
                    .HasColumnName("DatumToegevoegd")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.KlantId).HasColumnName("klantId");

                entity.Property(e => e.Status)
                    .HasColumnName("Status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Tafel).HasColumnName("Tafel");

                entity.Property(e => e.Tijd).HasColumnName("Tijd");

                entity.HasOne(d => d.Klant)
                    .WithMany(p => p.Reserveringen)
                    .HasForeignKey(d => d.KlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservering_Klant");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
