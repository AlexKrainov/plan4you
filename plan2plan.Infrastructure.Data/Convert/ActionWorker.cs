using plan2plan.Domain.Core;
using plan2plan.Domain.Core.ModelView;
using plan2plan.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan2plan.Infrastructure.Data.Convert
{
    public class ActionWorker
    {
        public action action;

        private IActionRepository actionRepository;
        private IFileRepository fileRepository;
        private string ip;

        public ActionWorker(IActionRepository actionRepository,
            IFileRepository fileRepository,
            string ip)
        {
            this.actionRepository = actionRepository;
            this.fileRepository = fileRepository;
            this.ip = ip;
            this.action = new action();
        }

        public async Task<action> GetActionCheckSheets(int userID = -1)
        {
            List<File> files = fileRepository.GetFiles().ToList();

            for (int i = 0; i < files.Count; i++)
            {
                Guid fileID = files[i].ID;
                action.check_sheets.Add(new check_sheet
                {
                    id = fileID.ToString(), //.Replace("-", ""),
                    likes = actionRepository.GetCountLikesByFileID(fileID),
                    downloads = actionRepository.GetCountDownloadsByFileID(fileID),
                    is_like = actionRepository.IsLikeByIP(ip, fileID)
                });
            }

            return action;
        }


    }
}
