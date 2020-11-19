using System;
using System.Collections.Generic;
using System.Text;

namespace FileForgeDP.Database.Dto
{
    public class FileOverviewDto
    {
        public string Id { get; set; }


        public string GroupId { get; set; }

        public string Description { get; set; }


        public string FileName { set; get; }

        public string ContentType { get; set; }

        public string LastModificationDate { get; set; }

        public string LastModifiedBy { get; set; }
    }
}
