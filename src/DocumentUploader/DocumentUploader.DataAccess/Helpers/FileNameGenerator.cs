using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentUploader.DataAccess.Helpers
{
    public class FileNameGenerator : IFileNameGenerator
    {
        public string Generate(string ext)
        {
            return Guid.NewGuid().ToString("N") + ext;
        }
    }
}
