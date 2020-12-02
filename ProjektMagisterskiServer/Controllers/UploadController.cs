using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http.Headers;
using ProjektMagisterskiServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel;

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
                ApplicationUser user = await GetActualUserAsync(_userManager);
                var req = Request;
                var imageDetailsString = Request.Form["detailsOfImage"];
                ImageModel imageModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ImageModel>(imageDetailsString);
                var cropPropertiesString = Request.Form["cropproperites"];
                ImageCropProp cropProperties = Newtonsoft.Json.JsonConvert.DeserializeObject<ImageCropProp>(cropPropertiesString);
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

                string processedImgPath = this.CreateImageOperation(fileName,user,TypeOfProcessing.Progowanie,cropProperties);

                var image = new Image();

                    image.ImageID = Guid.NewGuid();
                    image.Description = imageModel.Description;
                    image.ImgPath = imageModel.ImgPath;
                    image.Length = imageModel.Length;
                    image.Width = imageModel.Width;
                    image.Name = imageModel.Name;
                    image.ImgPath = dbPath;
                    image.ProcessedImgPath = processedImgPath;
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


        private string CreateImageOperation(string originalImageName, ApplicationUser user, TypeOfProcessing typeOfProcessing, ImageCropProp cropProperties)
        {
            //ProcessStartInfo start = new ProcessStartInfo();

            //string nazwaPliku="";

            //switch (typeOfProcessing)
            //{
            //    case TypeOfProcessing.Progowanie:
            //        nazwaPliku = "Thresholding";
            //        break;
            //    case TypeOfProcessing.RedukcjaPoziomowSzarosci:
            //        nazwaPliku = "ExcludeGray";
            //        break;
            //    default:
            //        break;
            //}

            //string sourceFile = $@"Resources\{nazwaPliku}.exe";
            //string destinationFile = $"Resources\\Images\\{user.UserName}\\{nazwaPliku}.exe";
            //System.IO.File.Copy(sourceFile, destinationFile, true);

            ////start.FileName = $@"G:\ProjektMagisterski\ProjektMagisterskiServer\ProjektMagisterskiServer\Resources\Thresholding.exe";
            //start.FileName = $@"G:\ProjektMagisterski\ProjektMagisterskiServer\Segmentation\dist\Thresholding.exe";
            ////start.FileName = destinationFile;
            //string[] args = { "", "", "" };
            ////args[0] = "C:\\Users\\Maciek\\Desktop\\Mag\\TEST\\TEST\\TEST.py";
            ////args[0] = $"G:\\ProjektMagisterski\\ProjektMagisterskiServer\\Segmentation\\{nazwaPliku}.py";
            //args[0] = $"G:\\ProjektMagisterski\\ProjektMagisterskiServer\\Segmentation\\Thresholding.py";

            //string userPath = @"G:\ProjektMagisterski\ProjektMagisterskiServer\ProjektMagisterskiServer\Resources\Images\" + user.UserName+ @"\";
            //args[1] = userPath + originalImageName;
            //string processedImage = originalImageName.Replace(".jpg", "Przetworzone.png");
            //args[2] = userPath + processedImage;
            //start.Arguments = string.Format("{0} {1} {2} {3} {4} {5} {6}", args[0], args[1], args[2], cropProperties.x1, cropProperties.x2, cropProperties.y1, cropProperties.y2);
            //start.UseShellExecute = false;
            //start.RedirectStandardOutput = true;
            //using (Process process = Process.Start(start))
            //{
            //    using (StreamReader reader = process.StandardOutput)
            //    {
            //        string result = reader.ReadToEnd();

            //    }
            //}
            //System.IO.File.Delete(destinationFile);
            //return $@"Resources\Images\{user.UserName}\{processedImage}";

            string nazwaPliku = "";

            switch (typeOfProcessing)
            {
                case TypeOfProcessing.Progowanie:
                    nazwaPliku = "Thresholding";
                    break;
                case TypeOfProcessing.RedukcjaPoziomowSzarosci:
                    nazwaPliku = "ExcludeGray";
                    break;
                default:
                    break;
            }
            string userPath = @"\Resources\Images\" + user.UserName + @"\";

            string userExeFile = userPath + $"{nazwaPliku}.exe";

            System.IO.File.Copy($@"G:\ProjektMagisterski\ProjektMagisterskiServer\Segmentation\dist\{nazwaPliku}.exe", $@"G:\ProjektMagisterski\ProjektMagisterskiServer\ProjektMagisterskiServer{userExeFile}", true);


            ProcessStartInfo start = new ProcessStartInfo();
                //start.FileName = @"C:\\Users\\Maciek\\Desktop\\PracaMagisterska\\TEST.exe";
                start.FileName = $@"G:\ProjektMagisterski\ProjektMagisterskiServer\ProjektMagisterskiServer{userExeFile}";
                string[] args = { "", "", "" };
                //args[0] = "C:\\Users\\Maciek\\Desktop\\Mag\\TEST\\TEST\\TEST.py";
                args[0] = @"G:\ProjektMagisterski\ProjektMagisterskiServer\Segmentation\Thresholding.py";

            //string userPath = @"\Resources\Images\" + user.UserName + @"\";
            //args[1] = userPath + originalImageName;
            //args[1] = @"C:\Users\Maciek\Desktop\PracaMagisterska\ZaznaczoneKolor.jpg";
            string processedImage = originalImageName.Replace(".jpg", "Przetworzone.png");
            args[1] = $@"G:\ProjektMagisterski\ProjektMagisterskiServer\ProjektMagisterskiServer\Resources\Images\{user.UserName}\{originalImageName}";

            //args[2] = userPath + processedImage;
            args[2] = $@"G:\ProjektMagisterski\ProjektMagisterskiServer\ProjektMagisterskiServer\Resources\Images\{user.UserName}\{processedImage}";

            //args[2] = "C:\\Users\\Maciek\\Desktop\\PracaMagisterska\\GOTOWaaaasssE.png";
                //args[2] = @"C:\Users\Maciek\Desktop\PracaMagisterska\GOTOWaaaasfvddvfE.png";

                start.Arguments = string.Format("{0} {1} {2} {3} {4} {5} {6}", args[0], args[1], args[2], cropProperties.x1, cropProperties.x2, cropProperties.y1, cropProperties.y2);
                start.UseShellExecute = false;
                start.RedirectStandardOutput = true; 
                using (Process process = Process.Start(start))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        string result = reader.ReadToEnd();
                    var x = 2;
                    }
                }
            System.IO.File.Delete($@"G:\ProjektMagisterski\ProjektMagisterskiServer\ProjektMagisterskiServer{userExeFile}");

            return $@"Resources\Images\{user.UserName}\{processedImage}";
        }

        private enum TypeOfProcessing
        {
            [Description("Brak Operacji Przetwarzania")]
            Brak,
            [Description("Progowanie")]
            Progowanie,
            [Description("Redukcja Poziomów Szarości")]
            RedukcjaPoziomowSzarosci,
            [Description("Metoda k-średnich")]
            KSrednich
        }
    }
}
