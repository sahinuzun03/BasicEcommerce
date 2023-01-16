using Autofac;
using Autofac.Extensions.DependencyInjection;
using EcommerceApp.Application.IoC;
using EcommerceApp.Infrastructure.Context;
using EcommerceApp.MVC.Models.SeedDataModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.{
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ECommerceAppDbContext>(_ =>
{
    _.UseSqlServer(builder.Configuration.GetConnectionString("EcommerceConnString"));
});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new DependencyResolver());
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(_ =>
{
    _.LoginPath = "/Login/Login";
    _.Cookie = new CookieBuilder
    {
        Name = "EcommerceCookie",
        SecurePolicy = CookieSecurePolicy.Always,
        HttpOnly = true //Client tarafında cookie görünür oluyor
    };
    _.ExpireTimeSpan = TimeSpan.FromMinutes(15);
    _.SlidingExpiration = false;
    _.Cookie.MaxAge = _.ExpireTimeSpan;
});

builder.Services.AddSession(_ =>
{
    _.IdleTimeout = TimeSpan.FromMinutes(15);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


SeedData.Seed(app);


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
  name: "areas",
  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
