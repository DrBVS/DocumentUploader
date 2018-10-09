using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DocumentUploader.Models
{
    public class FileModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }
        public FileType Type { get; set; }
        public string Path { get; set; }
    }
}
