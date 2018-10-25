using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleBlogAppCore.Extensions
{
    public static class FileUploadExtension
    {
        public static string GetFileExtension(this IFormFile formFile)
        {
            string p = formFile.FileName;

            string path = p.Substring(p.LastIndexOf(@"\") + 1);

            return path.Substring(path.LastIndexOf(".") + 1);
        }

        public static string GetOnlyFileName(this IFormFile formFile)
        {
            string p = formFile.FileName;

            string path = p.Substring(p.LastIndexOf(@"\") + 1);

            return path.Substring(0, path.IndexOf("."));
        }

        public static async Task UploadAsync(this IFormFile formFile,IHostingEnvironment _hostingEnvironment)
        {
            string photoPath = formFile.FileName;

            string fileName = formFile.GetOnlyFileName()+"_"+DateTime.Now.ToString("yyyyMMddHHmmss")+"."+ formFile.GetFileExtension();


            string fullFilePath = Path.Combine(_hostingEnvironment.ContentRootPath, "Uploads", fileName);

            using (FileStream fs = new FileStream(fullFilePath, FileMode.Create))
            {
                await formFile.CopyToAsync(fs);
            }
        }
    }
}
