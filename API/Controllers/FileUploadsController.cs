using System;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using API.Dtos;
using API.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    public class FileUploadsController : BaseApiController
    {
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            var config = builder.Build();

            string filePath = config.GetSection("FTPAddress1").Value + "/temp/";
            string fileHost = config.GetSection("FileHost1").Value + "/temp/";

            string errorMsg = "";

            if (!CommonFunctions.CreateFolder(filePath, out errorMsg))
            {
                return BadRequest();
            }
             var fileNameToSAve = Guid.NewGuid() + Path.GetExtension(file.ContentDisposition).Trim('"');
                    var fullPath = Path.Combine(filePath, fileNameToSAve);
                    var dbPath = Path.Combine(fileHost, fileNameToSAve);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    var fileUploadReturnDto = new FileUploadReturnDto();
                    fileUploadReturnDto.FilePath = fullPath.Substring(35);
                    fileUploadReturnDto.FullFilePath = dbPath;
                    return Ok(fileUploadReturnDto);
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
    }
}