using ImageMagick;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;
using Yamaanco.Application.Common.Options;
using Yamaanco.Application.Interfaces;
using Yamaanco.Domain.Enums;
using Yamaanco.Infrastructure.Shared.Extensions;

namespace Yamaanco.Infrastructure.Shared.Files
{
    public class ImageService : IImageService
    {
        private readonly AppOptions _appSettings;

        public ImageService(IOptions<AppOptions> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        private int GetImageSizeValue(PhotoSize size)
        {
            switch (size)
            {
                case PhotoSize.Large:
                    return _appSettings.LargeImageSize;

                case PhotoSize.Medium:
                    return _appSettings.MediumImageSize;

                case PhotoSize.Small:
                    return _appSettings.SmallImageSize;

                default:
                    return 0;
            }
        }

        public async Task<int> Upload(IFormFile formFile, string path, string aliasName, PhotoSize resizeTo)
        {
            var directoryPath = $"{_appSettings.DefaultUploadFolderName}\\{path}";

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

            //delete image if it exist (Update image case).
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            var bytes = await formFile.GetBytes();
            var length = 0;
            using (var stream = File.Create(filePath))
            {
                if (resizeTo != PhotoSize.Default)
                {
                    var resizeBytes = Resize(bytes, GetImageSizeValue(resizeTo));
                    length = resizeBytes.Length;
                    await stream.WriteAsync(resizeBytes);
                }
                else
                {
                    await stream.WriteAsync(bytes);
                }
            }

            return length;
        }

        public byte[] Read(Stream photo)
        {
            byte[] photoStream = null;
            using (var fileStream = photo)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fileStream.CopyTo(ms);
                    photoStream = ms.ToArray();
                }
            }
            return photoStream;
        }

        private byte[] Resize(byte[] image, int size)
        {
            byte[] afterProceccing = null;
            using (var img = new MagickImage(image))
            {
                img.Resize(size, size);
                afterProceccing = img.ToByteArray();
            }
            return afterProceccing;
        }
    }
}