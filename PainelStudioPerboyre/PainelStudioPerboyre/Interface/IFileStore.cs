using System;
using System.Collections.Generic;
using System.Text;

namespace PainelStudioPerboyre.Interface
{
    public interface IFileStore
    {
        string GetFilePath();

        string GetFilePath(string file);
    }
}
