using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;
using ST10028058_CLDV6212_POE_Final.Models;
using System;
using System.Linq;
using ST10028058_CLDV6212_POE_Final.Data;

namespace ST10028058_CLDV6212_POE_Final.Controllers
{
    public class FileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public FileController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: File/Upload
        public IActionResult Upload()
        {
            return View();
        }

        // POST: File/Upload
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(FileModel fileModel)
        {
            if (fileModel.UploadedFile != null && fileModel.UploadedFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsFolder); // Ensure the folder exists
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + fileModel.UploadedFile.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Save the file to the server
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await fileModel.UploadedFile.CopyToAsync(fileStream);
                }

                // Save file info in the database
                fileModel.Name = fileModel.UploadedFile.FileName;
                fileModel.Size = fileModel.UploadedFile.Length;
                fileModel.LastModified = DateTimeOffset.Now;
                _context.Files.Add(fileModel);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(fileModel);
        }

        // GET: File/Index
        public IActionResult Index()
        {
            var files = _context.Files.ToList();
            return View(files);
        }
    }
}
