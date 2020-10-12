using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjektMagisterskiServer.Models;

namespace ProjektMagisterskiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : InternalController
    {
        private UserManager<ApplicationUser> _userManager;
        public UserProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        [Authorize]
        public async Task<Object> GetUserProfile()
        {
            ApplicationUser _actualUser = await GetActualUserAsync(_userManager);
            return new
            {
                _actualUser.FullName,
                _actualUser.Email,
                _actualUser.UserName
            };
        }
    }
}