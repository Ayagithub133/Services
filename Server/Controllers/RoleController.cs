using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Server.DataAccessLayer;
using Services.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Server.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _role;
        private readonly UserManager<ApplicationUser> _userIdentity;
        private readonly IUserRepository<ApplicationUser> _user;
        public RoleController(RoleManager<IdentityRole> role,
            IUserRepository<ApplicationUser> user,
            UserManager<ApplicationUser> userIdentity)
        {
            _role = role;
            _user = user;
            _userIdentity = userIdentity;
        }

        [HttpGet("addsuper")]
        public  async Task<ActionResult> CreateRolesandUsers()
        {
            bool x = await _role.RoleExistsAsync("SuperAdmin");
            if(!x)
            {
                var role = new IdentityRole();
                role.Name = "SuperAdmin";
                await _role.CreateAsync(role);

                var user = await _user.FindByIdAsync("49a89180-f5b4-4c5d-b45f-d312c1cc5700");
                if(user!=null)
                {
                    await _userIdentity.AddToRoleAsync(user, "SuperAdmin");
                }
               else
                {

                }
            }
            return Ok();
        }
    }
}
