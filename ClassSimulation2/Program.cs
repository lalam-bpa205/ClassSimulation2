using ClassSimulation2.DAL.Context;
using ClassSimulation2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ClassSimulation2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<DAL.Context.AppDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
     
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;

            }).AddEntityFrameworkStores<AppDbContext>();
            var app = builder.Build();

            app.MapControllerRoute(
                name: "areas",
                pattern:"{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
                );
            app.MapControllerRoute(
               name: "default",
               pattern: "{controller=Home}/{action=Index}/{id?}"
               );



            app.UseStaticFiles();
            app.Run();
        }
    }
}
