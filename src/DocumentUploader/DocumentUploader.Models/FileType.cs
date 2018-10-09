namespace DocumentUploader.Models
{
    public enum FileType
    {
        Image,
        Document,
        None = 9999
    }

    public static class FileTypeExtensions
    {
        public static FileType GetFileType(this string ext)
        {
            switch (ext)
            {
                case ".jpg":
                case ".jpeg":
                case ".png":
                    return FileType.Image;
                case ".doc":
                case ".docx":
                    return FileType.Document;
                default:
                    return FileType.None;
            }
        }
    }
}
