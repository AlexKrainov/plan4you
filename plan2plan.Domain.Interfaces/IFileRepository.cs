using plan2plan.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan2plan.Domain.Interfaces
{
    public interface IFileRepository
    {
        void Create(File file);
        void Update(File file);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>return path + file name for delete</returns>
        string Delete(Guid id);
        Task<File> GetFileByIDTask(Guid id);
        File GetFileByID(Guid id);
        void Save();
        /// <summary>
        /// Получение файлов, где isDelete == false && isShow == true && isExist == true
        /// </summary>
        /// <returns></returns>
        IEnumerable<File> GetFiles();
        IEnumerable<File> GetAllFiles();
        IEnumerable<File> GetFilesByRange(int start, int count);
        void SetExistFile(File file, bool isExist);
        void CreateFilePreview(File file, string pathToFile, string previewType);
    }
}
