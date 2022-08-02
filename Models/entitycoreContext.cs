using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UrlShortener.Models
{
    public partial class entitycoreContext : DbContext
    {
        public entitycoreContext()
        {
        }

        public entitycoreContext(DbContextOptions<entitycoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Item> Items { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = Environment.GetEnvironmentVariable("pgsql_connection_string");

                //optionsBuilder.UseNpgsql("Host=localhost;Database=entitycore;Username=postgres;Password=su_root");
                optionsBuilder.UseNpgsql(connectionString);

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("item");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .UseCollation("C");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
