using BussPushNotification.Controllers;
using BussPushNotification.Data;
using BussPushNotification.Data.Interface;
using BussPushNotification.Data.Repository;
using BussPushNotification.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BussPushNotification
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<BussNotificationContext>(opts =>
                opts.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
            );
            builder.Services.AddIdentity<IdentityUser, IdentityRole>().
                AddEntityFrameworkStores<BussNotificationContext>();
            builder.Services.AddScoped<IUserRepository, SQLUserRepository>();
            builder.Services.Configure<IdentityOptions>(opts =>
            {
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = true;
                opts.Password.RequireUppercase = true;
                opts.Password.RequireDigit = true;
            });
            var app = builder.Build();
            // ��������� ����������� � ���� ������
            //TODO: ����� �������
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<BussNotificationContext>();
                if (context.Database.CanConnect())
                {
                    Console.WriteLine("����������� � ���� ������ ������� �����������.");
                }
                else
                {
                    Console.WriteLine("�� ������� ������������ � ���� ������.");
                }
            }
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.MapDefaultControllerRoute();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");

                // Add the new About controller and action to the routing system
                endpoints.MapControllerRoute(
                    name: "about",
                    pattern: "About",
                    defaults: new { controller = "Home", action = "AboutPage" });

                endpoints.MapControllerRoute(
                    name: "login",
                    pattern: "Login",
                    defaults: new { controller = "Authentication", action = "Login" });

                endpoints.MapControllerRoute(
                    name: "registration",
                    pattern:"SignUp",
                    defaults: new {controller = "Authentication", action = "SignUpForm"}
                    );
            });

            app.MapRazorPages();

            app.Run();
        }
    }
}