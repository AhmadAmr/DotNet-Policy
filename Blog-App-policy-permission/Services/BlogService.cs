using Blog_App_policy_permission.Data;
using Blog_App_policy_permission.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Blog_App_policy_permission.Services
{
    public class BlogService
    {
        private ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public BlogService(ApplicationDbContext context , IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        public async Task<IList<Blog>> GetBlogs()
        {
            return await _context.Blogs.ToListAsync();
        }

        public string UploadedFile(IFormFile HeaderImage)
        {
            string ImgName = null;

            if (HeaderImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "img");
                ImgName = Guid.NewGuid().ToString() + "_" + HeaderImage.FileName;
                string filePath = Path.Combine(uploadsFolder, ImgName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    HeaderImage.CopyTo(fileStream);
                }
            }
            return ImgName;
        }

    }
}
