namespace FileForgeDP.Database.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Defines the <see cref="FileModel" />.
    /// </summary>
    public class FileModel
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the GroupId.
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// Gets or sets the FileName.
        /// </summary>
        public string FileName { set; get; }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ContentType.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the File.
        /// </summary>
        public byte[] File { get; set; }

        public string LastModificationDate { get; set; }

        public string LastModifiedBy { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileModel"/> class.
        /// </summary>
        public FileModel()
        {
        }
    }
}
