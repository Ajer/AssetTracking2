using AssetTracking2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracking2.Data
{
    public class AssetContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-00V01P49;Initial Catalog=AssetTracking;Integrated Security=True;Connect Timeout=30;Encrypt=False;");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>()
                .HasDiscriminator(a => a.Type);

            base.OnModelCreating(modelBuilder);

            var assets = new List<Asset>
            {
                new Asset{
                    Id=1,
                    Type="Computer",
                    Brand = "Dell",
                    PurchaseDate = new DateTime(2024,7,1),
                    Model = "XPS",
                    PriceInDollar = 524 
                },
                new Asset{
                    Id=3,
                    Type="Phone",
                    Brand = "Iphone",
                    PurchaseDate = new DateTime(2024,6,15),
                    Model = "8",
                    PriceInDollar = 524
                },
                new Asset{
                    Id=2,
                    Type="Computer",
                    Brand = "MacBook",
                    PurchaseDate = new DateTime(2023,5,30),
                    Model = "Air",
                    PriceInDollar = 524
                },

                new Asset{
                    Id=4,
                    Type="Phone",
                    Brand = "Samsung",
                    PurchaseDate = new DateTime(2023,10,12),
                    Model = "fold",
                    PriceInDollar = 524
                }

            };

            modelBuilder.Entity<Asset>().HasData(assets);
        }

        public DbSet<Asset> Assets { get; set; }
    }
}
