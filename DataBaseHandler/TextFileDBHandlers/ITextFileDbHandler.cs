using DataBaseHandler.Models;
using System.Collections.Generic;

namespace DataBaseHandler
{
    public interface ITextFileDbHandler
    {
        List<string> ReadFileTitles();

        void AddFile(FileModel file);

        void UpdateFile(FileModel oldfile, FileModel newFile);

        void DeleteFile(FileModel file);

        FileModel GetFile(string fileTitle);
    }
}