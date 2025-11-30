using CommunityToolkit.Maui.Storage;
using ProcessScaleGenerator.Shared.Injections.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessScaleGenerator.Sevices.Implementation
{
    public class FolderStorage : IFolderStorage
    {
        public async Task<string> GetAllFilesInFolder()
        {
            var result = await FolderPicker.Default.PickAsync();

            if (!result.IsSuccessful)
                return string.Empty;

            return result.Folder.Path;
        }
        public async Task<string> GetSingleFileInFolder()
        {
            var result = await FilePicker.Default.PickAsync();

            if (result is null)
                return string.Empty;

            return result.FullPath;
        }
    }
}
