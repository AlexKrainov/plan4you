using plan2plan.Domain.Core;
using plan2plan.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan2plan.Infrastructure.Data.Components
{
    public class FileRepository : IFileRepository
    {
        private plat2platContext context;

        public FileRepository(plat2platContext _context)
        {
            this.context = _context;
        }
        public void Create(Domain.Core.File file)
        {
            context.Files.Add(file);
        }

        public string Delete(Guid id)
        {
            File file = context.Files.Find(id);

            if (file != null)
            {
                string path = file.Path;
                //file.isDelete = true;
                context.Files.Remove(file);
                return path;
            }

            return "";
        }

        public async Task<Domain.Core.File> GetFileByIDTask(Guid id)
        {
            return await context.Files.FindAsync(id);
        }

        public File GetFileByID(Guid id)
        {
            return context.Files.Find(id);
        }

        /// <summary>
        /// Возращает файлы с пометкой isDelete = false && isShow = true
        /// </summary>
        /// <returns></returns>
        public IEnumerable<File> GetFiles()
        {
            //List<File> files = 
            //for (int i = 0; i < files.Count(); i++)
            //{
            //    files[i].ID = files[i].ID;
            //}
            return context.Files.Where(x => x.isDelete == false && x.isShow == true && x.isExist == true);
        }

        /// <summary>
        /// Возращает файлы с пометкой isDelete = false && isShow = true
        /// и в диапазоне от start в количестве count
        /// </summary>
        /// <param name="start">Скакого индекса начинать</param>
        /// <param name="count">Количество объектов</param>
        /// <returns></returns>
        public IEnumerable<File> GetFilesByRange(int start, int count)
        {
            var files = context.Files.Where(x => x.isDelete == false && x.isShow == true).OrderBy(x => x.ID).ToList();

            if (start + count < files.Count)
            {
                return files.GetRange(start, count);
            }
            else
            {
                return files.GetRange(start, files.Count - start);
            }

        }
        public void SetExistFile(File file, bool isExist)
        {
            var fileFromDB = context.Files.FirstOrDefault(x => x.ID == file.ID);

            if (fileFromDB != null && fileFromDB.isExist == null || fileFromDB.isExist != isExist)
            {
                file.isExist = isExist;
                fileFromDB.isExist = isExist;
                this.Save();
            }
        }
        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Domain.Core.File file)
        {
            context.Entry(file).State = EntityState.Modified;
        }

        public void CreateFilePreview(File file, string pathToFile, string previewType)
        {
            var fileFromDB = context.Files.FirstOrDefault(x => x.ID == file.ID);

            if (fileFromDB != null)
            {
                if (previewType.ToLower() == "min")
                {
                    fileFromDB.PreviewPath_min = pathToFile;
                    file.PreviewPath_min = pathToFile;
                }
                else if (previewType.ToLower() == "avg")
                {
                    fileFromDB.PreviewPath_avg = pathToFile;
                    file.PreviewPath_avg = pathToFile;
                }
                else if (previewType.ToLower() == "max")
                {
                    fileFromDB.PreviewPath_max = pathToFile;
                    file.PreviewPath_max = pathToFile;
                }

                this.Save();
            }
        }

        public IEnumerable<File> GetAllFiles()
        {
            return context.Files;
        }
    }
}
