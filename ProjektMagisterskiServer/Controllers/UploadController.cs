using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjektMagisterskiServer.Models;

namespace ProjektMagisterskiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly AuthenticationContext _contex;

        public UploadController(AuthenticationContext contex)
        {
            _contex = contex;
        }

        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });
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

        [HttpPost, DisableRequestSizeLimit]
        [Route("AddImage")]
        public IActionResult AddImageToUser([FromBody]ImageModel imageModel)
        {
            try
            {
                if (imageModel == null)
                {
                    return BadRequest("Obiekt- zdjęcie nie istnieje");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Niepoprawny obiekt");
                }

                var image = new Image();

                //image.ImageID = Guid.NewGuid();
                image.Description = imageModel.Description;
                image.ImgPath = imageModel.ImgPath;
                image.Length = imageModel.Length;
                image.Width = imageModel.Width;
                image.Name = imageModel.Name;
                image.TypeOfProcessing = imageModel.TypeOfProcessing;
                var user = _contex.ApplicationUsers.Where(x => x.UserName == imageModel.UserName).FirstOrDefault();
                image.ApplicationUserId = user.Id;
                _contex.Add(image);
                _contex.SaveChanges();
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Wewnętrzny błąd serwera: {ex}");
            }
        }
    }
}