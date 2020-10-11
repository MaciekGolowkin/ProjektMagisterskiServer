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
        private UserManager<ApplicationUser> _userManager;

        public InternalController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        protected async Task<ApplicationUser> GetActualUserAsync()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            return await _userManager.FindByIdAsync(userId);
        }
    }
}