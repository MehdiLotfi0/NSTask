using NSTask.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Update.Internal;

namespace NSTask.Models
{
    public partial class NSTaskDataBase : DbContext
    {
        public NSTaskDataBase()
        {
        }

        public NSTaskDataBase(DbContextOptions<NSTaskDataBase> options)
            : base(options)
        {
        }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       => optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=NSTaskDataBase;Integrated Security=True");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {

                entity.ToTable("Product");
                entity.Property(e => e.ProductId).ValueGeneratedOnAdd();
                entity.Property(e => e.Name).HasMaxLength(150);
                entity.Property(e => e.ProductDate).HasColumnType("datetime");
                entity.Property(e => e.ManufacturePhone).HasMaxLength(150);
                entity.Property(e => e.ManufactureEmail).HasMaxLength(150);
                entity.Property(e => e.IsAvailable).HasMaxLength(150);

            });
            modelBuilder.Entity<Product>(entity =>
            {

                entity.ToTable("Product");
                entity.HasIndex(u => u.ManufactureEmail).IsUnique();
                entity.HasIndex(u => u.ProductDate).IsUnique();


            });
       
            OnModelCreatingPartial(modelBuilder);

        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
