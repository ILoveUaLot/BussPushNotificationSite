using BussPushNotification.Controllers;
using BussPushNotification.Data;
using BussPushNotification.Data.Interface;
using BussPushNotification.Data.Repository;
using BussPushNotification.Infrastructure;
using BussPushNotification.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json;

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
            builder.Services.AddMemoryCache();
            builder.Services.AddDbContext<BussNotificationContext>(opts =>
                opts.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
            );
            builder.Services.AddIdentity<IdentityUser, IdentityRole>().
                AddEntityFrameworkStores<BussNotificationContext>();
            builder.Services.AddScoped<IUserRepository, SQLUserRepository>();
            builder.Services.AddScoped<IApiRepository, SQLApiRepository>();
            builder.Services.AddTransient<IGeoService, GeoService>();
            builder.Services.Configure<IdentityOptions>(opts =>
            {
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = true;
                opts.Password.RequireUppercase = true;
                opts.Password.RequireDigit = true;
                opts.User.RequireUniqueEmail = true;
            });
            builder.Services.Configure<CookieAuthenticationOptions>(
                IdentityConstants.ApplicationScheme,
                opts =>
                {
                    opts.LoginPath = "/Authentication/Login";
                    opts.AccessDeniedPath = "/Account/Views/Accessdenied";
                });
            builder.Services.AddSwaggerGen();

            builder.Services.AddHttpClient("station_listAPI", client =>
            {
                client.BaseAddress = new Uri("https://api.rasp.yandex.net/v3.0/stations_list/");
            });
            builder.Services.AddHttpClient("schedule", client =>
            {
                client.BaseAddress = new Uri("https://api.rasp.yandex.net/v3.0/schedule/");
            });
            builder.Services.AddHttpClient("geocoder", client =>
            {
                client.BaseAddress = new Uri("https://geocode-maps.yandex.ru/1.x/");
            });
            builder.Services.Configure<RouteApiSettings>(builder.Configuration.GetSection("RouteApiSettings"));
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
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.MapDefaultControllerRoute();
            app.UseRouting();
            app.UseHttpMethodOverride();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");

                endpoints.MapControllerRoute(
                    name: "about",
                    pattern: "/About",
                    defaults: new { controller = "Home", action = "AboutPage" });

                endpoints.MapControllerRoute(
                    name: "login",
                    pattern: "/Login",
                    defaults: new { controller = "Authentication", action = "Login" });

                endpoints.MapControllerRoute(
                    name: "registration",
                    pattern: "/SignUp",
                    defaults: new {controller = "Authentication", action = "SignUpForm"}
                    );
            });

            app.MapRazorPages();

            app.Run();
        }
    }
}