using System;
using bootcamp_store_backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace bootcamp_store_backend.Infraestructure.Persistence
{
	public class StoreContext:DbContext
	{
		public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .HasOne(i => i.Category)
                .WithMany()
                .HasForeignKey(i => i.CategoryId)
                .IsRequired();
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}

