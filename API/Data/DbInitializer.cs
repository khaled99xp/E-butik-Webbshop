using API.Models;
using API.Helpers;
using System.Collections.Generic;

namespace API.Data
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Users.Any())
            {
                var adminUser = new User
                {
                    Id = 1,
                    Name = "Admin",
                    Email = "admin@example.com",
                    PasswordHash = PasswordHelper.HashPassword("Admin@123"),
                    IsAdmin = true
                };
                context.Users.Add(adminUser);
                context.SaveChanges();
            }

            if (!context.Categories.Any())
            {
                var category1 = new Category { Id = 1, Name = "Kategori 1" };
                var category2 = new Category { Id = 2, Name = "Kategori 2" };
                var category3 = new Category { Id = 3, Name = "Kategori 3" };

                context.Categories.AddRange(category1, category2, category3);
                context.SaveChanges();
            }

            if (!context.Products.Any())
            {
                var products = new List<Product>
                {
                    new Product
                    {
                        Id = 1,
                        Name = "Produkt 1",
                        Description = "Beskrivning av produkt 1",
                        Price = 100,
                        Stock = 10,
                        CategoryId = 1,
                        ImageUrl = "https://placehold.in/600"
                    },
                    new Product
                    {
                        Id = 2,
                        Name = "Produkt 2",
                        Description = "Beskrivning av produkt 2",
                        Price = 150,
                        Stock = 20,
                        CategoryId = 1,
                        ImageUrl = "https://placehold.in/600"
                    },
                    new Product
                    {
                        Id = 3,
                        Name = "Produkt 3",
                        Description = "Beskrivning av produkt 3",
                        Price = 200,
                        Stock = 15,
                        CategoryId = 2,
                        ImageUrl = "https://placehold.in/600"
                    },
                    new Product
                    {
                        Id = 4,
                        Name = "Produkt 4",
                        Description = "Beskrivning av produkt 4",
                        Price = 250,
                        Stock = 5,
                        CategoryId = 2,
                        ImageUrl = "https://placehold.in/600"
                    },
                    new Product
                    {
                        Id = 5,
                        Name = "Produkt 5",
                        Description = "Beskrivning av produkt 5",
                        Price = 300,
                        Stock = 8,
                        CategoryId = 3,
                        ImageUrl = "https://placehold.in/600"
                    },
                    new Product
                    {
                        Id = 6,
                        Name = "Produkt 6",
                        Description = "Beskrivning av produkt 6",
                        Price = 350,
                        Stock = 12,
                        CategoryId = 3,
                        ImageUrl = "https://placehold.in/600"
                    }
                };

                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }
    }
}
