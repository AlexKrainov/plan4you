namespace plan2plan.Domain.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class File
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public string PreviewPath_min { get; set; }
        public string PreviewPath_avg { get; set; }
        public string PreviewPath_max { get; set; }
        public bool isDelete { get; set; }
        public bool isShow { get; set; }
        public Nullable<bool> isExist { get; set; }
    }
}
