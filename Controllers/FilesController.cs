using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10028058_CLDV6212_POE_Final.Data;
using ST10028058_CLDV6212_POE_Final.Models;

namespace ST10028058_CLDV6212_POE_Final.Controllers
{
    public class FilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FilesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Files.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,LastModified,UploadedFile")] FileModel fileModel)
        {
            if (ModelState.IsValid)
            {
                if (fileModel.UploadedFile != null)
                {
                    // Save the file content as a byte array (optional)
                    using (var memoryStream = new MemoryStream())
                    {
                        await fileModel.UploadedFile.CopyToAsync(memoryStream);
                        fileModel.Size = fileModel.UploadedFile.Length;
                        fileModel.Name = fileModel.UploadedFile.FileName;
                        fileModel.LastModified = DateTimeOffset.Now;
                        // Optional: Save byte array to the database, depending on your implementation
                        // fileModel.FileContent = memoryStream.ToArray();
                    }
                }

                _context.Add(fileModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fileModel);
        }
    }
}
