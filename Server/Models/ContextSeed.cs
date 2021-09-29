using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Server.Models
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Models.Roles.User.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Models.Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Models.Roles.Admin.ToString()));
        }

        public static async Task seedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var user = await userManager.FindByIdAsync("49a89180-f5b4-4c5d-b45f-d312c1cc5700");
            if(user == null)
            {
                var defaultUser = new ApplicationUser
                {
                    Id = "49a89180-f5b4-4c5d-b45f-d312c1cc5700",
                    UserName = "superadmin",
                    FirstName = "SuperAdmin",
                    SecondName = "SuperAdmin",
                    LastName = "SuperAdmin",
                    Email= "superadmin@default.com",
                    PasswordHash = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes("123a$$word.")),
                    EmailConfirmed = true,
                    PhoneNumber = "1234567891",
                    PhoneNumberConfirmed = true,
                    CreateAt = DateTime.Now
                };
                await userManager.CreateAsync(defaultUser);
                await userManager.AddToRoleAsync(defaultUser, Models.Roles.User.ToString());
                await userManager.AddToRoleAsync(defaultUser, Models.Roles.Admin.ToString());
                await userManager.AddToRoleAsync(defaultUser, Models.Roles.SuperAdmin.ToString());
            }
            else
            {
                if(! await userManager.IsInRoleAsync(user, Models.Roles.SuperAdmin.ToString()))
                    await userManager.AddToRoleAsync(user, Models.Roles.SuperAdmin.ToString());
               
                if (!await userManager.IsInRoleAsync(user, Models.Roles.User.ToString()))
                    await userManager.AddToRoleAsync(user, Models.Roles.User.ToString());

                if (!await userManager.IsInRoleAsync(user, Models.Roles.Admin.ToString()))
                    await userManager.AddToRoleAsync(user, Models.Roles.Admin.ToString());
            }
        }
    }
}
