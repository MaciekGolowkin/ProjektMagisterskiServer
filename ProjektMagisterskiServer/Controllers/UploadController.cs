using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http.Headers;
using ProjektMagisterskiServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using IronPython.Hosting;
using System.Diagnostics;

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
                //this.CreateImageOperation();
                ApplicationUser user = await GetActualUserAsync(_userManager);
                var req = Request;
                var imageDetailsString = Request.Form["detailsOfImage"];
                ImageModel imageModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ImageModel>(imageDetailsString);
                var file = Request.Form.Files[0];
                var partialFolderName= Path.Combine("Resources", "Images");
                var folderName = Path.Combine(partialFolderName, user.UserName);
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

                this.CreateImageOperation(@"C:\Users\Maciek\Desktop\PracaMagisterska\Glowny.JPG", "pobranePrzetworzone.png");

                //MatlabConnector.MatlabConnection.WyciecieSzarosci();
                //Ima

                var image = new Image();

                    image.ImageID = Guid.NewGuid();
                    image.Description = imageModel.Description;
                    image.ImgPath = imageModel.ImgPath;
                    image.Length = imageModel.Length;
                    image.Width = imageModel.Width;
                    image.Name = imageModel.Name;
                    image.ImgPath = dbPath;
                    image.TypeOfProcessing = imageModel.TypeOfProcessing;
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


        private void CreateImageOperation(string originalImageName,string processedImageName)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @"C:\\Users\\Maciek\\Desktop\\PracaMagisterska\\TEST.exe";
            string[] args = { "", "", "" };
            args[0] = "C:\\Users\\Maciek\\Desktop\\Mag\\TEST\\TEST\\TEST.py";
            args[1] = @"C:\Users\Maciek\Desktop\PracaMagisterska\ZaznaczoneKolor.JPG";
            args[2] = @"C:\Users\Maciek\Desktop\PracaMagisterska\ostatecznycheck.png";
            //pass these to your Arguements property of your ProcessStartInfo instance

            //start.Arguments = string.Format("{0} {1} {2}", "C:\\Users\\Maciek\\Documents\\Visual Studio 2017\\Projects\\TEST\\TEST\\TEST.py", "C:\\Users\\Maciek\\Desktop\\PracaMagisterska\\ZaznaczoneKolor.JPG", "C:\\Users\\Maciek\\Desktop\\PracaMagisterska\\KONCOWY.png");
            start.Arguments = string.Format("{0} {1} {2}", args[0], args[1], args[2]);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();

                }
            }

            //string sourceFile = @"Resources\WyciecieSzarosci.m";
            ////string destinationFile = @"Resources\Images\Asiunia\WyciecieSzarosci.m";
            ////System.IO.File.Copy(sourceFile, destinationFile, true);

            //MLApp.MLApp matlab = new MLApp.MLApp();
            ////matlab.Execute(@"cd Resources\Images\Asiunia\WyciecieSzarosci.m");
            //matlab.Execute(@"cd C:\Users\Maciek\Desktop\MatlabTest");
            //object result = null;

            //matlab.Feval("WyciecieSzarosci", 1, out result, originalImageName, processedImageName);
            //System.IO.File.Delete(destinationFile);


            //MLApp.MLApp matlab = new MLApp.MLApp();
            //matlab.Execute(@"cd C:\Users\Maciek\Desktop\MatlabTest");
            //object result = null;

            //matlab.Feval("WyciecieSzarosci", 1, out result, @"C:\Users\Maciek\Desktop\PracaMagisterska\Glowny.JPG", "aafddfssa.png");
            //object[] res = result as object[];

            //return "s";
        }

    }
}
