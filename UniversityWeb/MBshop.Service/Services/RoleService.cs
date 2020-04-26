using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBshop.Data.Data;
using MBshop.Service.interfaces;
using MBshop.Service.OutputModels;
using MBshop.Service.StaticProperyes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MBshop.Service.Services
{
    public class RoleService : IRoleService
    {
        private readonly MovieShopDBSEContext db;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly string adminRole = "Admin";
        private readonly string userRole = "User";
        private readonly string Moderator = "Moderator";

        public RoleService(MovieShopDBSEContext db,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            this.db = db;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public List<OutputRoleAssignModel> GetRoles()
        {

            //var dbUsers = this.db.AspNetUserRoles.Select(x => x.User.UserName);

            var model = new List<OutputRoleAssignModel>();

            // get role types
            var result = this.db
                .AspNetUserRoles
                .ToList();

            //get users
            var users = this.db
                .AspNetUsers
                .ToList();

            //get assignet user roles
            var roles = this.db
                .AspNetRoles
                .ToList();
            if (result != null && users != null && roles != null)
            {

                for (int i = 0; i < result.Count(); i++)
                {
                    var tempRes = result[i];
                    for (int j = 0; j < users.Count(); j++)
                    {
                        if (tempRes.User.UserName == users[j].UserName)
                        {

                            var userRole = roles
                                .Where(x => x.Id == tempRes.RoleId)
                                .Select(x => x.Name)
                                .FirstOrDefault();

                            OutputRoleAssignModel roleModel = new OutputRoleAssignModel
                            {
                                userId = users[j].Id,
                                Users = users[j].UserName,
                                UserRole = userRole
                            };

                            model.Add(roleModel);

                        }
                    }
                }
            }
            return model;
        }

        public async Task<string> AssignRole(string roleId, string userId, string userName, string userRole)
        {
            var role = this.db.AspNetUserRoles.Where(x => x.UserId == userId).FirstOrDefault();

            var roleses = db.AspNetUserRoles.Include(a => a.Role).Include(a => a.User);

            var roleAssign = roleses.Where(x => x.UserId == userId).FirstOrDefault();

            var rolesColl = new string[3];
            rolesColl[0] = this.adminRole;
            rolesColl[1] = this.userRole;
            rolesColl[2] = this.Moderator;

            if (userId != role.UserId)
            {
                return $"Not found";
            }

            var user = await userManager.FindByNameAsync(userName);

            if (roleAssign.Role.Name != null)
            {
                await userManager.RemoveFromRolesAsync(user, rolesColl);
            }

            var admin = await roleManager.RoleExistsAsync(this.adminRole);
            var usery = await roleManager.RoleExistsAsync(this.userRole);
            var moderat = await roleManager.RoleExistsAsync(this.Moderator);

            if (roleAssign.Role.Name == this.adminRole)
            {
                var urs = await this.userManager.RemoveFromRoleAsync(user, this.adminRole);
            }
            else if (roleAssign.Role.Name == this.userRole)
            {
                var urs = await this.userManager.RemoveFromRoleAsync(user, this.userRole);
            }
            else if (roleAssign.Role.Name == this.Moderator)
            {
                var urs = await this.userManager.RemoveFromRoleAsync(user, this.Moderator);
            }

            if (userRole == this.adminRole)
            {
                await this.roleManager.CreateAsync(new IdentityRole { Name = this.adminRole });

                await this.userManager.AddToRoleAsync(user, adminRole);

            }
            else if (userRole == this.Moderator)
            {
                await this.roleManager.CreateAsync(new IdentityRole { Name = this.Moderator });

                await this.userManager.AddToRoleAsync(user, this.Moderator);
            }
            else if (userRole == this.userRole)
            {
                await this.roleManager.CreateAsync(new IdentityRole { Name = this.userRole });

                await this.userManager.AddToRoleAsync(user, userRole);
            }

            GlobalAlertMessages.StatusMessage = $"This user {user.UserName} has been assign to {userRole} role !";

            return $"Success";
        }
    }
}
