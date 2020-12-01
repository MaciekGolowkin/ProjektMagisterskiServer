using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjektMagisterskiServer.Models;

namespace ProjektMagisterskiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : InternalController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuthenticationContext _contex;


        public ImagesController(AuthenticationContext contex, UserManager<ApplicationUser> userManager)
        {
            _contex = contex;
            _userManager = userManager;
        }

        [Route("GetImagesRegistry")]
        [Authorize]
        [HttpGet]
        public async Task<ImageModel[]> GetRegistryOfIagesAsync()
        {
            ApplicationUser user = await GetActualUserAsync(_userManager);
            var images = _contex.ApplicationImages.Where(x => x.ApplicationUserID == user.Id);

            var userImages = images
                        .Select(i => new ImageModel { ImgPath = i.ImgPath.Replace("\\","//"),
                            ProcessedImgPath = i.ProcessedImgPath.Replace("\\", "//"),
                            Name = i.Name,
                            Description = i.Description, Length = i.Length,
                            TypeOfProcessing = i.TypeOfProcessing, Width = i.Width })
                        .Distinct()
                        .OrderByDescending(i => i.Name)
                        .ToArray();
            return userImages;
        }

        [Route("GetImagesRegistry/{id}")]
        [Authorize]
        [HttpDelete]
        public ActionResult DeleteImage(Guid id)
        {
            var image = _contex.ApplicationImages.Where(x => x.ImageID == id).FirstOrDefault();
            if (image == null) NotFound($"Wskazany obraz o id : {id} nie istnieje");

            _contex.ApplicationImages.Remove(image);
            _contex.SaveChanges();

            return Ok("Zdjęcie zostało usunięte pomyślnie");
        }

    }
}