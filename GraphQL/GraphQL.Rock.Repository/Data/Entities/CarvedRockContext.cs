using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GraphQL.Rock.Repository.Data.Entities
{
    public partial class CarvedRockContext : DbContext
    {
        public CarvedRockContext()
        {
        }

        public CarvedRockContext(DbContextOptions<CarvedRockContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ProductReview> ProductReviews { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=CarvedRock;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<ProductReview>(entity =>
            {
                entity.HasIndex(e => e.ProductId);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductReviews)
                    .HasForeignKey(d => d.ProductId);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PhotoFileName).HasMaxLength(100);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            });
        }
    }
}
