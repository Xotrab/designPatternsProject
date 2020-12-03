using FileForgeDP.Database.Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace FileForgeDP.Database.Repositories
{
    public class WorkspaceRepository
    {
        
        private readonly IMongoCollection<WorkspaceModel> mWorkspaces;
        private readonly FileRepository mFileService;
        public WorkspaceRepository(IFileForgeDatabaseSettings settings, FileRepository fileService)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            mFileService = fileService;
            mWorkspaces = database.GetCollection<WorkspaceModel>(settings.WorkspaceCollectionName);
            
        }
        public string Create(WorkspaceModel workspace)
        {
            mWorkspaces.InsertOne(workspace);

            return workspace.Id;
        }
        public string Create(FileModel file)
        {
            mFileService.Create(file);

            return file.Id;
        }

        public List<WorkspaceModel> Get() =>
            mWorkspaces.Find(workspace => true).ToList();


        public WorkspaceModel Get(string Id) =>
            mWorkspaces.Find(workspace => workspace.Id == Id).FirstOrDefault();

        // Get the File with fileId from workspace with workspaceId
        public FileModel GetFile(string workspaceId, string fileId) => mFileService.Get(workspaceId, fileId);


        public void Update(string id, WorkspaceModel workspaceModel) =>
            mWorkspaces.ReplaceOne(workspace => workspace.Id == id, workspaceModel);

        public void Remove(string id)
        {
            mFileService.RemoveSet(id);
            mWorkspaces.DeleteOne(workspace => workspace.Id == id);
        }

        public void RemoveOne(string workspaceId, string fileId)
        {
            mFileService.Remove(workspaceId, fileId);
        }

    }


}

