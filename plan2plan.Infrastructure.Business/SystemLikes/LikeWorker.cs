using plan2plan.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan2plan.Infrastructure.Business.SystemLikes
{
    public class LikeWorker
    {

        public string IP { get; }

        private IActionRepository actionRepository;
        private string fileID;
        private FileType fileType;

        public LikeWorker(IActionRepository actionRepository, string ip, string typeFile, string fileID)
        {
            this.actionRepository = actionRepository;
            this.IP = ip;
            this.fileID = fileID;

            switch (typeFile)
            {
                case "check_sheets":
                    fileType = FileType.check_sheets;
                    break;
                default:
                    fileType = FileType.none;
                    break;
            }
        }

        public async Task<int> Update()
        {
            Guid id;
            if (fileType != FileType.none
                && Guid.TryParse(fileID, out id) == true)
            {
                switch (fileType)
                {
                    case FileType.check_sheets:
                        actionRepository.CreateOrUpdate(IP, id);
                        return await actionRepository.SaveAsync();
                    case FileType.none:
                        break;
                    default:
                        break;
                }

            }
            return 0;
        }
    }
}
