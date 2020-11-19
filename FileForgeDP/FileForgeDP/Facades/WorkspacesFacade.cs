using FileForgeDP.Database.Dto;
using FileForgeDP.Database.Repositories;
using FileForgeDP.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileForgeDP.Facades
{
    public class WorkspacesFacade
    {
        private readonly WorkspaceRepository mWorkspaceRepository;
        private readonly FileRepository mFileRepository;
        private readonly FileModelMapper mFileModelMapper;
        private readonly WorkspaceModelMapper mWorkspaceModelMapper;
        public WorkspacesFacade(WorkspaceRepository workspaceRepository, FileRepository fileRepository, FileModelMapper fileModelMapper, WorkspaceModelMapper workspaceModelMapper) 
        {
            this.mWorkspaceRepository = workspaceRepository;
            this.mFileRepository = fileRepository;
            this.mFileModelMapper = fileModelMapper;
            this.mWorkspaceModelMapper = workspaceModelMapper;
        }


        public string AddWorkspace(WorkspaceModelDto workspaceModelDto)
        {
            var workspaceToSave = mWorkspaceModelMapper.DtoToWorkspaceModel(workspaceModelDto);

            var createdId = mWorkspaceRepository.Create(workspaceToSave);
            
            return createdId;
        }

        public string AddFileToWorkspace(FileModelDto fileModelDto)
        {
            var fileModel = mFileModelMapper.DtoToFileModel(fileModelDto);
            var createdId = mWorkspaceRepository.Create(fileModel);

            return createdId;
        }


        public List<FileOverviewDto> GetWorkspaceFiles(string workspaceId)
        {
            var result = mFileRepository.Get(workspaceId);

            return result;
        }

        public FileModelDto GetFileFromWorkspace(string workspaceId, string fileId)
        {
            var result = mWorkspaceRepository.GetFile(workspaceId, fileId);

            return mFileModelMapper.FileModelToDto(result);
        }

        public WorkspaceModelDto GetWorkspace(string id)
        {
            var result = mWorkspaceRepository.Get(id);

            return mWorkspaceModelMapper.WorkspaceModelToDto(result);
        }

        // TODO add return deleted ID
        public void DeleteWorkspace(string id)
        {
            mWorkspaceRepository.Remove(id);
        }

        public void RemoveFileFromWorkspace(string workspaceId, string fileId)
        {
            mWorkspaceRepository.RemoveOne(workspaceId, fileId);
        }
        // TODO add return of updated workspace
        public void UpdateWorkspace(string id, WorkspaceModelDto workspaceModelDto)
        {
            var workspaceModel = mWorkspaceModelMapper.DtoToWorkspaceModel(workspaceModelDto);

            mWorkspaceRepository.Update(id, workspaceModel);
        }

        public void UpdateWorkspaceFile(string workspaceId, string fileId, FileModelDto fileModelDto)
        {
            var fileModel = mFileModelMapper.DtoToFileModel(fileModelDto);

            mFileRepository.Update(fileId, fileModel);
        }
    }

    


}
