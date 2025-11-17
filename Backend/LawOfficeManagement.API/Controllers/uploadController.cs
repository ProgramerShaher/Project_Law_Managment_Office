using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LawOfficeManagement.API.Controllers
{
    [ApiController]
    public class uploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public uploadController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [Route("api/upload/{folder}")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<object> Upload(string folder)
        {
            var userid = 1;
            List<string> paths = new List<string>();
            var files = Request.Form.Files;

            foreach (var file in files)
            {
                if (file == null || file.Length == 0)
                    continue;

                int MaxContentLength = 1024 * 1024 * 20; // 10MB
                IList<string> AllowedFileExtensions = new List<string>
        {
            ".jpg", ".jpeg", ".gif", ".png", ".mp4", ".pdf", ".doc", ".docx", ".xlsx"
        };

                var ext = Path.GetExtension(file.FileName).ToLower();
                if (!AllowedFileExtensions.Contains(ext))
                {
                    return new
                    {
                        iserror = true,
                        myresult = "Please upload file of allowed type: .jpg, .gif, .png, .mp4, .pdf, .doc, .docx, .xlsx"
                    };
                }
                if (file.Length > MaxContentLength)
                {
                    return new
                    {
                        iserror = true,
                        myresult = "Please upload file of allowed size is less than requierd " + MaxContentLength / 1024 / 1024 + "MB"
                    };
                }

                string webRootPath = _hostingEnvironment.WebRootPath;
                var path = "/upload/" + folder + "/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                var filePath = webRootPath + "" + path;
                if (!Directory.Exists(Path.Combine(webRootPath, "upload", folder.ToString())))
                {
                    Directory.CreateDirectory(Path.Combine(webRootPath, "upload", folder.ToString()));
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // معالجة الصور فقط إذا أكبر من 10MB
                //if ((ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif") && file.Length > MaxContentLength)
                //{
                //    using (var inputStream = file.OpenReadStream())
                //    {
                //        //var compressedBytes = UniversalImageCompressor.CompressImage(inputStream, 1024, 1024, 85L);
                //        await System.IO.File.WriteAllBytesAsync(filePath, inputStream);
                //    }
                //}
                //else
                //{
                //    using (var stream = new FileStream(filePath, FileMode.Create))
                //    {
                //        await file.CopyToAsync(stream);
                //    }
                //}

                paths.Add(path);
            }

            if (paths.Count > 0)
                return paths;

            return new
            {
                iserror = true,
                myresult = "No valid files uploaded."
            };
        }

    }
}
