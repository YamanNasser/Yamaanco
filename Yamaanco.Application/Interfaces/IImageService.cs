using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using Yamaanco.Domain.Enums;

namespace Yamaanco.Application.Interfaces
{
    public interface IImageService
    {
        byte[] Read(Stream photo);

        Task<int> Upload(IFormFile formFile, string path, string aliasName, PhotoSize resizeTo);
    }
}