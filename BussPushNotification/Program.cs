using BussPushNotification.Controllers;

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
            
            var app = builder.Build();

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
                    defaults: new { controller = "Login", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "registration",
                    pattern:"SignUp",
                    defaults: new {controller = "Login", action = "SignUpForm"}
                    );
            });
            

            app.MapRazorPages();

            app.Run();
        }
    }
}