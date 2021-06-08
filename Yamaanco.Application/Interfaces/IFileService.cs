using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Yamaanco.Application.Interfaces
{
    public interface IFileService
    {
        Task<(byte[], string, string)> Download(string file);

        Task<int> Upload(IFormFile formFile, string path, string aliasName);
    }
}