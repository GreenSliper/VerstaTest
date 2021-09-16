using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace VerstaTest.Models
{
    public partial class versta_testContext : DbContext
    {
        public versta_testContext()
        {
        }

        public versta_testContext(DbContextOptions<versta_testContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Russian_Russia.1251");

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreationTime)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("creationtime")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.PackageWeight).HasColumnName("packageweight");

                entity.Property(e => e.ReceiveDate)
                    .HasColumnType("date")
                    .HasColumnName("receivedate");

                entity.Property(e => e.ReceiverAddress)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("receiveraddress");

                entity.Property(e => e.ReceiverCity)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("receivercity");

                entity.Property(e => e.SenderAddress)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("senderaddress");

                entity.Property(e => e.SenderCity)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("sendercity");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
