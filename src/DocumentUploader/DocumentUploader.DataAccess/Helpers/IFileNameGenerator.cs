using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentUploader.DataAccess.Helpers
{
    public interface IFileNameGenerator
    {
        string Generate(string ext);
    }
}
