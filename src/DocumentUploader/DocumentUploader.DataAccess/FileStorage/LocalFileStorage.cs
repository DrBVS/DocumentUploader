using DocumentUploader.DataAccess.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DocumentUploader.DataAccess.FileStorage
{
    public class LocalFileStorage : IFileStorage
    {
        private readonly IHostingEnvironment environment;
        private readonly IFileNameGenerator nameGenerator;

        private readonly string ContentDirectory;

        public LocalFileStorage(IHostingEnvironment environment, IFileNameGenerator nameGenerator)
        {
            this.nameGenerator = nameGenerator;
            this.environment = environment;

            ContentDirectory = Path.Combine(environment.ContentRootPath, "/files");
            if (!Directory.Exists(ContentDirectory))
                Directory.CreateDirectory(ContentDirectory);
        }

        public async Task<string> SaveFileAsync(IFormFile file)
        {
            var fileName = Path.Combine(ContentDirectory, nameGenerator.Generate(Path.GetExtension(file.FileName)));
            using (var fileStream = new FileStream(fileName, FileMode.Create))
                await file.CopyToAsync(fileStream);

            return fileName;
        }

        public string SaveFile(IFormFile file)
        {
            var fileName = Path.Combine(ContentDirectory, Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName));
            using (var fileStream = new FileStream(fileName, FileMode.Create))
                file.CopyTo(fileStream);

            return fileName;
        }
    }
}
