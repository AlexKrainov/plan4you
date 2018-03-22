
using PdfiumViewer;
using plan2plan.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace plan2plan.Common.Image
{
    using Model_File = plan2plan.Domain.Core.File;
    public class ImageGenerator
    {
        private IFileRepository fileRepository;
        private HttpServerUtilityBase server;

        public ImageGenerator(IFileRepository fileRepository, HttpServerUtilityBase server)
        {
            this.fileRepository = fileRepository;
            this.server = server;
        }

        #region Создание если нет превью и проверка существование файла 
        public void UpdateFiles()
        {
            RefreshFiles(fileRepository.GetFiles().ToList());
        }

        /// <summary>
        /// Проверяем существует ли pdf файл, если нет записываем isExist = false, иначе создаем превью 
        /// </summary>
        /// <param name="files"></param>
        private void RefreshFiles(IList<Model_File> files)
        {
            foreach (Model_File file in files)
            {
                bool isPreviewReady = false;
                bool isExists = ExistsFile(file);

                if (isExists) //Если pdf файл существует
                {
                    #region create min preview
                    if (string.IsNullOrEmpty(file.PreviewPath_min) || new FileInfo(server.MapPath(file.PreviewPath_min)).Exists == false) //Если превью файл не существует - создаем
                    {
                        isPreviewReady = CreatePreview(file, "preview_" + file.FileName + "_min.jpg", "min");
                    }
                    #endregion

                    #region create avg preview
                    if (string.IsNullOrEmpty(file.PreviewPath_avg) || new FileInfo(server.MapPath(file.PreviewPath_avg)).Exists == false)
                    {
                        isPreviewReady = CreatePreview(file, "preview_" + file.FileName + "_avg.jpg", "avg");
                    }
                    #endregion
                }
            }
            fileRepository.Save();
        }

        /// <summary>
        /// создаем превью файл
        /// </summary>
        /// <param name="file"></param>
        /// <param name="previewFileName">Название превью файла, пример preview_Report_min.jpg</param>
        /// <returns></returns>
        private bool CreatePreview(Model_File file, string previewFileName, string previewType)
        {
            var previewPath = server.MapPath("~/Source/Preview/" + previewFileName);
            bool isPreviewReady = CreatePreviewFile(server.MapPath("../" + file.Path), previewPath, PreviewSizeType.GetSize(previewType)); //PdfManager.

            if (isPreviewReady == true) //Если получилось создать превью файл сохраняем полный путь в базу
            {
                fileRepository.CreateFilePreview(file, "/Source/Preview/" + previewFileName, previewType);
            }

            return isPreviewReady;
        }


        private bool CreatePreviewFile(string pdfPath, string previewPath, Size size)
        {
            bool result = false;
            try
            {
                using (var document = PdfDocument.Load(System.IO.File.OpenRead(pdfPath)))
                {
                    var image = document.Render(0, size.Width, size.Height, 1, 1, true);
                    image.Save(previewPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                result = true;
            }
            catch (Exception ex)
            {
                // handle exception here;
            }
            return result;
        }

        /// <summary>
        /// Проверяет существует ли файл в заданной директории
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        protected bool ExistsFile(Model_File file)
        {
            var path = server.MapPath("../" + file.Path);
            var fileInfo = new FileInfo(path);

            fileRepository.SetExistFile(file, fileInfo.Exists);

            return fileInfo.Exists;
        }
        #endregion

    }
}