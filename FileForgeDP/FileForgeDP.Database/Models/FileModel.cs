namespace FileForgeDP.Database.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class FileModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string GroupId { get; set; }

        public string FileName { set; get; }

        public string Description { get; set; }

        public string ContentType { get; set; }

        public byte[] File { get; set; }

        public string LastModificationDate { get; set; }

        public string LastModifiedBy { get; set; }
    }
}
