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
        void Delete(File file);
        Task<File> GetFileByID(Guid id);
        void Save();
        IEnumerable<File> GetFiles();
        IEnumerable<File> GetFilesByRange(int start, int count);
        void SetExistFile(File file, bool isExist);
    }
}
