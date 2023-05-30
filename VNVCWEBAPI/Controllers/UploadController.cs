using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VNVCWEBAPI.Common.Config;
using VNVCWEBAPI.Common.Constraint;
using VNVCWEBAPI.Common.Models;
using static System.Net.Mime.MediaTypeNames;

namespace VNVCWEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        public UploadController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }
        [HttpPost("[Action]")]
        //[Authorize(Roles = Permissions.Upload.Create)]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var index = file.FileName.LastIndexOf('.');
                var fileName = file.FileName.Substring(0, index) + DateTime.Now.Ticks + file.FileName.Substring(index);
                string directoryPath = Path.Combine(webHostEnvironment.ContentRootPath + "//wwwroot//", UploadConfig.Images);
                string filePath = Path.Combine(directoryPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = $"/{UploadConfig.Images.Substring(0, UploadConfig.Images.Length - 1)}/{fileName}",
                    Messages = new string[] { "Upload hình thành công." }
                });
            }
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
                Messages = new string[] { "Upload hình thất bại." }
            });
        }

    }
}
