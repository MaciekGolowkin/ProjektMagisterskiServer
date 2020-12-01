using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProjektMagisterskiServer.Models
{
    public class ImageModel
    {
        [Column(TypeName = "nvarchar(100)")]
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string TypeOfProcessing { get; set; }

        public long Length { get; set; }

        public long Width { get; set; }

        public string ImgPath { get; set; }

        public string ProcessedImgPath { get; set; }
    }
}

