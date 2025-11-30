using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessScaleGenerator.Shared.Injections.Contract;

public interface IFolderStorage
{
    Task<string> GetSingleFileInFolder();
    Task<string> GetAllFilesInFolder();
}
