namespace FileForgeDP.Database.Dto
{
    using Microsoft.AspNetCore.Http;

    public class FileModelDto
    {
        public string Id { get; set; }

        public string GroupId { get; set; }

        public string Description { get; set; }

        public string FileName { set; get; }

        public IFormFile File { get; set; }

        public byte[] FileBytes { get; set; }
        public string ContentType { get; set; }

        public string LastModificationDate { get; set; }

        public string LastModifiedBy { get; set; }
    }
}
