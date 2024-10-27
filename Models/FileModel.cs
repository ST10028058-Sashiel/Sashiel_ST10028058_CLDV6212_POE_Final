using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace ST10028058_CLDV6212_POE_Final.Models
{
    public class FileModel
    {
        public int Id { get; set; } // Make sure there is a primary key for the database
        
        [Required]
        public string Name { get; set; }
        
        public long Size { get; set; }
        
        public DateTimeOffset? LastModified { get; set; }
        
        [Display(Name = "Upload File")]
        [NotMapped]
        public IFormFile UploadedFile { get; set; }  // New property to store the uploaded file temporarily

        public string DisplaySize
        {
            get
            {
                if (Size >= 1024 * 1024)
                    return $"{Size / 1024 / 1024} MB";
                if (Size >= 1024)
                    return $"{Size / 1024} KB";
                return $"{Size} Bytes";
            }
        }
    }
}
