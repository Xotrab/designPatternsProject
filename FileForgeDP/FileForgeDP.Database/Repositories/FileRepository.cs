namespace FileForgeDP.Database.Repositories
{
    using FileForgeDP.Database.Builders;
    using FileForgeDP.Database.Dto;
    using FileForgeDP.Database.Models;
    using MongoDB.Driver;
    using System.Collections.Generic;

    public class FileRepository
    {
        private readonly IMongoCollection<FileModel> mFiles;

        public FileRepository(IFileForgeDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            mFiles = database.GetCollection<FileModel>(settings.CollectionName);
        }

        public string Create(FileModel file)
        {
            mFiles.InsertOne(file);

            return file.Id;
        }

        public List<FileOverviewDto> Get(string workspaceId) =>
            mFiles.Find(file => file.GroupId == workspaceId)
                  .Project( x => new FileOverviewDto
                  {
                      FileName = x.FileName,
                      ContentType = x.ContentType,
                      Description = x.Description, 
                      GroupId = x.GroupId,
                      Id = x.Id,
                      LastModificationDate = x.LastModificationDate,
                      LastModifiedBy = x.LastModifiedBy
                  })
                  .ToList();

        public FileModel Get(string workspaceId,string fileId) =>
            mFiles.Find(file => file.Id == fileId & file.GroupId == workspaceId).FirstOrDefault(); 

        public void Update(string id, FileModel fileModel)
        {
            var update = MongoUpdateQueryBuilder<FileModel>.Builder();
            if (fileModel.FileName != "" )
            {
                update.Add("FileName", fileModel.FileName);
            }
            if (fileModel.Description != "")
            {
                update.Add("Description", fileModel.Description);
            }
            update.Add("LastModificationDate", fileModel.LastModificationDate);
            update.Add("LastModifiedBy", fileModel.LastModifiedBy);
            mFiles.UpdateOne(file => file.Id == fileModel.Id, update.Build());
        }

        public void Remove(FileModel modelIn) =>
            mFiles.DeleteOne(file => file.Id == modelIn.Id);

        public void Remove(string id) =>
            mFiles.DeleteOne(file => file.Id == id);

        public void Remove(string workspaceId, string fileId) =>
            mFiles.DeleteOne(file => file.Id == fileId && file.GroupId == workspaceId);

        public void RemoveSet(string groupId) =>
            mFiles.DeleteMany(file => file.GroupId == groupId);
    }
}
