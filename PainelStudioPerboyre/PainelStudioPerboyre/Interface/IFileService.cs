using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PainelStudioPerboyre.Interface
{
    public interface IFileService
    {
       void  SavePicture(string name, Stream data, string location = "temp");
        void Savefile(string name, MemoryStream data, string location = "temp");
    }
}
