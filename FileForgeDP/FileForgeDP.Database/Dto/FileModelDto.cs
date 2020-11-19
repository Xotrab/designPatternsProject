namespace FileForgeDP.Database.Dto
{
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Defines the <see cref="FileModelDto" />.
    /// </summary>
    public class FileModelDto
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the GroupId.
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the FileName.
        /// </summary>
        public string FileName { set; get; }

        /// <summary>
        /// Gets or sets the File.
        /// </summary>
        public IFormFile File { get; set; }

        public byte[] FileBytes { get; set; }
        public string ContentType { get; set; }

        public string LastModificationDate { get; set; }

        public string LastModifiedBy { get; set; }

    }
}
