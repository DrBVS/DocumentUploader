using DocumentUploader.Models;

namespace DocumentUploader.DataAccess
{
    public interface IDatabaseProvider
    {
        IDataRepository<FileModel> FileRepository { get; }
    }
}
