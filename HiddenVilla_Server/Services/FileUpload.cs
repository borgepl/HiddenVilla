﻿using HiddenVilla_Server.Services.Contracts;
using Microsoft.AspNetCore.Components.Forms;

namespace HiddenVilla_Server.Services
{
    public class FileUpload : IFileUpload
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public FileUpload(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }

        public bool DeleteFile(string fileName)
        {
            try
            {
                var path = $"{_webHostEnvironment.WebRootPath}\\RoomImages\\{fileName}";
                if (File.Exists(path))
                {
                    File.Delete(path);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> UploadFile(IBrowserFile file)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(file.Name);
                var fileName = Guid.NewGuid().ToString() + fileInfo.Extension;
                var folderDirectory = $"{_webHostEnvironment.WebRootPath}\\RoomImages";
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "RoomImages", fileName);

                var memoryStream = new MemoryStream();
                await file.OpenReadStream().CopyToAsync(memoryStream);

                if (!Directory.Exists(folderDirectory))
                {
                    Directory.CreateDirectory(folderDirectory);
                }

                await using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    memoryStream.WriteTo(fs);
                }
                var url = $"{_configuration.GetValue<string>("ServerUrl")}";
                var fullPath = $"{url}RoomImages/{fileName}";
                return fullPath;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
