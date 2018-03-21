using plan2plan.Domain.Core;
using plan2plan.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan2plan.Infrastructure.Data.Components
{
    using Action = plan2plan.Domain.Core.Action;
    public class ActionRepository : IActionRepository
    {
        private plat2platContext context;

        public ActionRepository(plat2platContext context)
        {
            this.context = context;
        }
        public void Create(Action action)
        {
            context.Actions.Add(action);
        }

        public IEnumerable<Action> GetActionsWithFileByFileID()
        {
            throw new NotImplementedException();
        }

        public int GetCountDownloadsByFileID(Guid fileID)
        {
            return context.Actions.Count(x => x.FileID == fileID && x.isDownload == true);
        }

        public int GetCountLikesByFileID(Guid fileID)
        {
            return context.Actions.Count(x => x.FileID == fileID && x.isLike == true);
        }

        public bool IsLikeByIP(string ip, Guid fileID)
        {
            Action action = context.Actions.FirstOrDefault(x => x.IP == ip && x.FileID == fileID);
            if (action != null)
            {
                return action.isLike;
            }
            return false;
        }

        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Update(string ip, Guid fileID)
        {
            Action action = context.Actions.FirstOrDefault(x => x.IP == ip && x.FileID == fileID);

            if (action != null)
            {
                action.isLike = !action.isLike;
            }
            else
            {
                this.Create(new Action
                {
                    IP = ip,
                    FileID = fileID,
                    isLike = true,
                    dateTime = DateTime.Now
                });
            }

        }
    }
}
