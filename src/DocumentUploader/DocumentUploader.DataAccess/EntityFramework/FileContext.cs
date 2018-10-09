using DocumentUploader.Models;
using Microsoft.EntityFrameworkCore;

namespace DocumentUploader.DataAccess.EntityFramework
{
    public class FileContext : DbContext
    {
        public DbSet<FileModel> Files { get; set; }
        public FileContext(DbContextOptions<FileContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
