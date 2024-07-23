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
            modelBuilder.Entity<Asset>().UseTptMappingStrategy();

            base.OnModelCreating(modelBuilder);

            var computers = new List<Computer>
            {
                new Computer{
                    Id=1,
                    Type="Computer",
                    Brand = "Dell",
                    PurchaseDate = new DateTime(2024,7,1),
                    Model = "XPS",
                    PriceInDollar = 524,
                    LocalPrice = 524*10.75,
                    OfficeId = 1
                },

                new Computer{
                    Id=2,
                    Type="Computer",
                    Brand = "MacBook",
                    PurchaseDate = new DateTime(2023,5,30),
                    Model = "Air",
                    PriceInDollar = 654,
                    LocalPrice = 654,
                    OfficeId = 2
                }
            };

            var phones = new List<Phone>
            {
                new Phone{
                    Id=3,
                    Type="Phone",
                    Brand = "Iphone",
                    PurchaseDate = new DateTime(2024,6,15),
                    Model = "8",
                    PriceInDollar = 298,
                    LocalPrice = 298*0.92,
                    OfficeId = 3
                },
                new Phone{
                    Id=4,
                    Type="Phone",
                    Brand = "Samsung",
                    PurchaseDate = new DateTime(2023,10,12),
                    Model = "fold",
                    PriceInDollar = 241,
                    LocalPrice = 241*10.75,
                    OfficeId = 1
                }

            };

            modelBuilder.Entity<Computer>().HasData(computers);
            modelBuilder.Entity<Phone>().HasData(phones);

            var offices = new List<Office>
            {
                new Office
                {
                    Id = 1,
                    City = "Stockholm",
                    Country = "Sweden",
                    CountryCode = "SWE",
                    Currency = "SEK"
                },
                new Office
                {
                    Id = 2,
                    City = "Buffalo",
                    Country = "USA",
                    CountryCode = "USA",
                    Currency = "USD"
                },
                new Office
                {
                    Id = 3,
                    City = "Madrid",
                    Country = "Spain",
                    CountryCode = "SPA",
                    Currency = "EUR"
                }
            };
            modelBuilder.Entity<Office>().HasData(offices);
        }

        public DbSet<Asset> Assets { get; set; }

        public DbSet<Computer> Computers { get; set; }

        public DbSet<Phone> Phones { get; set; }

        public DbSet<Office> Offices { get; set; }
    }
}
