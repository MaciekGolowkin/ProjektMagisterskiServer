using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjektMagisterskiServer.Models;

namespace ProjektMagisterskiServer.Controllers
{
    public class InternalController : ControllerBase
    {
        protected async Task<ApplicationUser> GetActualUserAsync(UserManager<ApplicationUser> _userManager)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            return await _userManager.FindByIdAsync(userId);
        }
    }
}