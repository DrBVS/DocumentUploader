using DocumentUploader.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DocumentUploader.DataAccess.EntityFramework
{
    public class EFFileRepository : IDataRepository<FileModel>
    {
        private readonly FileContext fileContext;

        public EFFileRepository(string connectionStr)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FileContext>();
            optionsBuilder.UseSqlServer(connectionStr);
            fileContext = new FileContext(optionsBuilder.Options);

            if (!fileContext.Files.Any())
            {
                fileContext.Files.AddRange(
                    new FileModel { Name = "File1.jpg", Size = 108024, Type = FileType.Image },
                    new FileModel { Name = "File2.docx", Size = 2708024, Type = FileType.Document },
                    new FileModel { Name = "File3.jpg", Size = 308024, Type = FileType.Image },
                    new FileModel { Name = "File4.jpg", Size = 458043, Type = FileType.Image },
                    new FileModel { Name = "File5.doc", Size = 6024, Type = FileType.Document },
                    new FileModel { Name = "File6.doc", Size = 378024, Type = FileType.Document },
                    new FileModel { Name = "File7.jpg", Size = 12108024, Type = FileType.Image },
                    new FileModel { Name = "File8.docx", Size = 708024, Type = FileType.Document }
                    );

                fileContext.SaveChanges();
            }
        }

        public void Delete(string id)
        {
            var file = fileContext.Files.FirstOrDefault(t => t.Id == id);
            fileContext.Files.Remove(file);
            fileContext.SaveChanges();
        }

        public IEnumerable<FileModel> GetAll()
        {
            return fileContext.Files.ToList();
        }

        public FileModel GetById(string id)
        {
            return fileContext.Files.FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<FileModel> Insert(IEnumerable<FileModel> item)
        {
            fileContext.Files.AddRange(item);
            fileContext.SaveChanges();

            return item;
        }

        public FileModel Update(FileModel item)
        {
            fileContext.Files.Update(item);
            fileContext.SaveChanges();

            return item;
        }
    }
}
