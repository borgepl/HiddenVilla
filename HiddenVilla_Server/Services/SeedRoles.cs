using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Microsoft.EntityFrameworkCore;
using DataAccess.Data;

namespace HiddenVilla_Server.Services
{
    public class SeedRoles
    {
        public static Task SeedUsersAndRoles(RoleManager<IdentityRole> roleManager,
           UserManager<IdentityUser> userManager, ApplicationDbContext db)
        {
            if (!roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
            {

                roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
                roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();

                // if roles are not created, then we craate admin user as well

                string adminEmail = "admin@localhost.com";
                userManager.CreateAsync(new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    PhoneNumber = "123456789",
                    EmailConfirmed = true
                }, "Admin123!").GetAwaiter().GetResult();

                string userEmail = "user@localhost.com";
                userManager.CreateAsync(new IdentityUser
                {
                    UserName = userEmail,
                    Email = userEmail,
                    PhoneNumber = "123456789",
                    EmailConfirmed = true
                }, "User123!").GetAwaiter().GetResult();

                IdentityUser adminUser = db.Users.FirstOrDefault(u => u.Email == adminEmail);
                userManager.AddToRoleAsync(adminUser, SD.Role_Admin).GetAwaiter().GetResult();

                IdentityUser user = db.Users.FirstOrDefault(u => u.Email == userEmail);
                userManager.AddToRoleAsync(user, SD.Role_Customer).GetAwaiter().GetResult();


            }

            return Task.CompletedTask;
        }
    }
}
