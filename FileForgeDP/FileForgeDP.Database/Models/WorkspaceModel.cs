using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FileForgeDP.Database.Models
{
    public class WorkspaceModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        public string Name { get; set; }
    }
}
