using System.IO;
using System.Collections.Generic;

namespace backup
{
    public interface ISettingsReader
    {
        Settings ReadSettings();
    }
}