using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan2plan.Domain.Interfaces
{
    using Action = plan2plan.Domain.Core.Action;
    public interface IActionRepository
    {
        void Create(Action action);
        Task<int> SaveAsync();

        IEnumerable<Action> GetActionsWithFileByFileID();
        int GetCountLikesByFileID(Guid fileID);
        int GetCountDownloadsByFileID(Guid fileID);
        int GetCountLikeByIP(string ip);
        int GetCountDownloadsByIP(string ip);
        /// <summary>
        /// Лайкнул ли данный IP файл
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="fileID"></param>
        /// <returns></returns>
        bool IsLikeByIP(string ip, Guid fileID);
        void CreateOrUpdate(string ip, Guid fileID);

    }
}
