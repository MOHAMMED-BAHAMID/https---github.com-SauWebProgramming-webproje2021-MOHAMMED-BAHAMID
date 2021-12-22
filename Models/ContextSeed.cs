using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebProje.Models;

public static class ContextSeed
{
    public enum Roles
{
    Admin,
    User,
    Yurt
}
    public static async Task SeedRolesAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        //Seed Roles
        await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Roles.Yurt.ToString()));
    }
    public static async Task SeedAdminAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
{
    //Seed Default User
    var defaultUser = new User 
    { 
        UserName = "Admin", 
        Email = "Admin@gmail.com",
        FName = "Admin",
        EmailConfirmed = true, 
    };
    if (userManager.Users.All(u => u.Id != defaultUser.Id))
    {
        var user = await userManager.FindByEmailAsync(defaultUser.Email);
        if(user==null)
        {
            await userManager.CreateAsync(defaultUser, "123Pa$$word.");
            await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
        }
               
    }
}
}       