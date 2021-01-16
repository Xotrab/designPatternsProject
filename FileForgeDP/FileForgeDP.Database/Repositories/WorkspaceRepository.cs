using FileForgeDP.Database.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileForgeDP.Database.Repositories
{
    public class WorkspaceRepository
    {
        private readonly IMongoCollection<WorkspaceModel> mWorkspaces;
        private readonly FileRepository mFileRepository;

        public WorkspaceRepository(IFileForgeDatabaseSettings settings, FileRepository fileRepository)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            mFileRepository = fileRepository;
            mWorkspaces = database.GetCollection<WorkspaceModel>(settings.WorkspaceCollectionName);
            
        }

        public string Create(WorkspaceModel workspace)
        {
            mWorkspaces.InsertOne(workspace);

            return workspace.Id;
        }

        public string Create(FileModel file)
        {
            mFileRepository.Create(file);

            return file.Id;
        }

        public void SynchronizeWorkspaces(IEnumerable<string> groupNames)
        {
            var databaseWorkspaceNames = Get().Select(x => x.Name);

            var newGroups = groupNames.Except(databaseWorkspaceNames);
            var removedGroups = databaseWorkspaceNames.Except(groupNames);

            foreach (var newGroup in newGroups)
            {
                var newWorkspace = new WorkspaceModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = newGroup
                };

                Create(newWorkspace);
            }

            foreach (var removedGroup in removedGroups)
            {
                RemoveByName(removedGroup);
            }
        }

        public List<WorkspaceModel> Get() =>
            mWorkspaces.Find(workspace => true).ToList();

        public WorkspaceModel Get(string Id) =>
            mWorkspaces.Find(workspace => workspace.Id == Id).FirstOrDefault();

        public WorkspaceModel GetByName(string name) =>
            mWorkspaces.Find(workspace => workspace.Name == name).FirstOrDefault();

        // Get the File with fileId from workspace with workspaceId
        public FileModel GetFile(string workspaceId, string fileId) =>
            mFileRepository.Get(workspaceId, fileId);

        public void Update(string id, WorkspaceModel workspaceModel) =>
            mWorkspaces.ReplaceOne(workspace => workspace.Id == id, workspaceModel);

        public void Remove(string id)
        {
            mFileRepository.RemoveSet(id);
            mWorkspaces.DeleteOne(workspace => workspace.Id == id);
        }

        public void RemoveByName(string name)
        {
            var workspace = GetByName(name);

            Remove(workspace.Id);
        }

        public void RemoveOne(string workspaceId, string fileId) =>
            mFileRepository.Remove(workspaceId, fileId);
    }
}

