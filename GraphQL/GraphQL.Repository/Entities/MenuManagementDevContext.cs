using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GraphQL.Repository.Entities
{
    public partial class MenuManagementDevContext : DbContext
    {
        public MenuManagementDevContext()
        {
        }

        public MenuManagementDevContext(DbContextOptions<MenuManagementDevContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Application> Application { get; set; }
        public virtual DbSet<NavigationMenu> NavigationMenu { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RoleNavigationMenu> RoleNavigationMenu { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=ICTDEVSQL03;Initial Catalog=MenuManagementDev;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Application>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.DomainUrl)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NavigationMenu>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ActionRoute)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.NavigationMenu)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NavigationMenu_Application1");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_NavigationMenu_NavigationMenu");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RoleNavigationMenu>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.NavigationMenuId).HasColumnName("NavigationMenuID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.NavigationMenu)
                    .WithMany(p => p.RoleNavigationMenu)
                    .HasForeignKey(d => d.NavigationMenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleNavigationMenu_NavigationMenu");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleNavigationMenu)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleNavigationMenu_Role");
            });
        }
    }
}
