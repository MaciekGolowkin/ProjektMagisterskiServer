using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http.Headers;
using ProjektMagisterskiServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace ProjektMagisterskiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : InternalController
    {
        private readonly AuthenticationContext _contex;
        private readonly UserManager<ApplicationUser> _userManager;

        public UploadController(AuthenticationContext contex, UserManager<ApplicationUser> userManager)
        {
            _contex = contex;
            _userManager = userManager;
        }


        [HttpPost, DisableRequestSizeLimit]
        [Authorize]
        public async Task<IActionResult> UploadAsync()
        {
            try
            {
                var req = Request;
                var imageDetailsString = Request.Form["detailsOfImage"];
                ImageModel imageModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ImageModel>(imageDetailsString);
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    int count = 0;
                    while (System.IO.File.Exists(fullPath))
                    {
                        count++;
                        fileName = fileName.Replace(".jpg",$"({count}).jpg");
                        fullPath = Path.Combine(pathToSave, fileName);
                    }
                    var dbPath = Path.Combine(folderName, fileName).Replace("\\", "//");
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }


                    if (imageModel == null)
                    {
                        return BadRequest("Obiekt- zdjęcie nie istnieje");
                    }

                    if (!ModelState.IsValid)
                    {
                        return BadRequest("Niepoprawny obiekt");
                    }

                    var image = new Image();

                    image.ImageID = Guid.NewGuid();
                    image.Description = imageModel.Description;
                    image.ImgPath = imageModel.ImgPath;
                    image.Length = imageModel.Length;
                    image.Width = imageModel.Width;
                    image.Name = imageModel.Name;
                    image.ImgPath = dbPath;
                    image.TypeOfProcessing = imageModel.TypeOfProcessing;

                    ApplicationUser user = await GetActualUserAsync(_userManager);

                    image.ApplicationUserID = user.Id;
                    _contex.Add(image);
                    _contex.SaveChanges();
                    return StatusCode(201);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
