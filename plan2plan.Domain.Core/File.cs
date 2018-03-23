namespace plan2plan.Domain.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class File
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        [DisplayName("Отображаемое имя")]
        public string Title { get; set; }
        [DisplayName("Описание")]
        public string Description { get; set; }
        [DisplayName("Имя при скачивании (без пробелов)")]
        public string FileName { get; set; }
        [DisplayName("Путь к файлу")]
        public string Path { get; set; }
        public string PreviewPath_min { get; set; }
        public string PreviewPath_avg { get; set; }
        public string PreviewPath_max { get; set; }
        [DisplayName("Удаленный файл")]
        [DefaultValue(false)]
        public bool isDelete { get; set; }
        [DefaultValue(true)]
        [DisplayName("Показывать ?")]
        public bool isShow { get; set; }
        public Nullable<bool> isExist { get; set; }
    }
}
