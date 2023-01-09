using EcommerceApp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using EcommerceApp.Domain.Entities;
using EcommerceApp.Domain.Enums;

namespace EcommerceApp.MVC.Models.SeedData
{
    public static class SeedData
    {
        public static void Seed(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<ECommerceAppDbContext>();

            dbContext.Database.Migrate();

            if (dbContext.Categories.Count() == 0)
            {
                dbContext.Categories.Add(new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Home Appliances",
                    CreateDate = DateTime.Now,
                    Status = Status.Active

                });
                dbContext.Categories.Add(new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Electronics",
                    CreateDate = DateTime.Now,
                    Status = Status.Active

                });
                dbContext.Categories.Add(new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Textile",
                    CreateDate = DateTime.Now,
                    Status = Status.Active

                });
            }

            if (dbContext.Employees.Count() == 0)
            {
                dbContext.Employees.Add(new Employee()
                {
                    Id = Guid.NewGuid(),
                    Name = "Şahin",
                    Surname = "Uzun",
                    EmailAddress = "sahin.uzun7@outlook.com",
                    Status = Status.Active,
                    Password = "1234",
                    CreateDate = DateTime.Now,
                    Roles = Roles.Admin,
                    BirthDate = new DateTime(1999, 10, 2)
                });
            }

            dbContext.SaveChanges();
        }
    }
}
