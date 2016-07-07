using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmegaTronOnionIO.Models
{

    public class FileListing
    {
        public List<FileEntry> entries { get; set; }
    }

    public class FileEntry
    {
        public string name { get; set; }
        public string type { get; set; }
    }

}
