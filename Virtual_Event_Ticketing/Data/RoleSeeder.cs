using Microsoft.AspNetCore.Identity;
using WebApplication1.Models;


namespace EventPortal.Data;


public static class RoleSeeder
{
    public static async Task SeedAsync(IServiceProvider provider)
    {
        var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = provider.GetRequiredService<UserManager<ApplicationUser>>();


        string[] roles = new[] { "Admin", "Organizer", "Attendee" };
        foreach (var r in roles)
        {
            if (!await roleManager.RoleExistsAsync(r))
                await roleManager.CreateAsync(new IdentityRole(r));
        }


// Create default admin if none exists
        var adminEmail = "admin@eventportal.local";
        var admin = await userManager.FindByEmailAsync(adminEmail);
        if (admin == null)
        {
            admin = new ApplicationUser { UserName = "admin", Email = adminEmail, EmailConfirmed = true };
            await userManager.CreateAsync(admin, "Admin#1234");
            await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}