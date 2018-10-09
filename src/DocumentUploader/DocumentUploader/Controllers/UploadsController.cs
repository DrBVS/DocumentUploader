using DocumentUploader.DataAccess;
using DocumentUploader.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentUploader.Controllers
{
    [Route("api/uploads")]
    public class UploadsController : Controller
    {
        private IDatabaseProvider dbProvider;
        private IFileStorage fileStorage;
        public UploadsController(IDatabaseProvider provider, IFileStorage fileStorage)
        {
            dbProvider = provider;
            this.fileStorage = fileStorage;
        }

        [HttpGet]
        public IEnumerable<FileModel> Get()
        {
            return dbProvider.FileRepository.GetAll();
        }

        [HttpGet("{id}")]
        public FileModel Get(string id)
        {
            return dbProvider.FileRepository.GetById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post(IFormFileCollection uploads)
        {
            var files = new List<FileModel>();
            foreach (var uploadFile in uploads)
            {
                var path = await fileStorage.SaveFileAsync(uploadFile);

                files.Add(new FileModel
                {
                    Name = uploadFile.FileName,
                    Size = uploadFile.Length,
                    Type = Path.GetExtension(uploadFile.FileName).GetFileType(),
                    Path = path
                });
            }

            if (files.Any())
                dbProvider.FileRepository.Insert(files);

            return Ok(files);
        }

        [HttpPut]
        public FileModel Put([FromBody]FileModel file)
        {
            return dbProvider.FileRepository.Update(file);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            dbProvider.FileRepository.Delete(id);
        }

    }
}
