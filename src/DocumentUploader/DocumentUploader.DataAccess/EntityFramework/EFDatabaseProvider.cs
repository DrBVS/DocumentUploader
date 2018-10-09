using DocumentUploader.Models;

namespace DocumentUploader.DataAccess.EntityFramework
{
    public class EFDatabaseProvider : IDatabaseProvider
    {
        public IDataRepository<FileModel> FileRepository { get; private set; }

        public EFDatabaseProvider(string connectionStr)
        {
            FileRepository = new EFFileRepository(connectionStr);
        }
    }
}
