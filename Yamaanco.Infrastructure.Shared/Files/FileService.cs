using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Yamaanco.Application.Common.Options;
using Yamaanco.Application.Interfaces;
using Yamaanco.Infrastructure.Shared.Extensions;

namespace Yamaanco.Infrastructure.Shared.Files
{
    public class FileService : IFileService
    {
        private readonly AppOptions _appSettings;

        public FileService(IOptions<AppOptions> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public async Task<int> Upload(IFormFile formFile, string path, string aliasName)
        {
            var directoryPath = $"{_appSettings.DefaultUploadFolderName}\\{path}";
            var length = (int)formFile.Length;

            if (formFile == null || formFile.Length == 0)
            {
                throw new Exception("file not selected");
            }

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), directoryPath, $"{ aliasName}{Path.GetExtension(formFile.FileName)}");

            //Create the directory if not exist.
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            //delete file if it exist (Update file case).
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            var bytes = await formFile.GetBytes();
            using (var stream = File.Create(filePath))
            {
                await stream.WriteAsync(bytes);
            }

            return length;
        }

        public async Task<(byte[], string, string)> Download(string file)
        {
            if (file == null)
            {
                throw new FileNotFoundException("filename not present");
            }

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           _appSettings.DefaultUploadFolderName, file);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return (memory.ToArray(), GetContentType(path), Path.GetFileName(path));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}