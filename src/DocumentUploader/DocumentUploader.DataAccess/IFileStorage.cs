using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DocumentUploader.DataAccess
{
    public interface IFileStorage
    {
        Task<string> SaveFileAsync(IFormFile file);
        string SaveFile(IFormFile file);
    }
}
