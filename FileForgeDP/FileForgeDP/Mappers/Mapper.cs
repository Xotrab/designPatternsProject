using FileForgeDP.Database.Dto;
using FileForgeDP.Database.Models;

namespace FileForgeDP
{
    public class Mapper
    {
        private readonly IOurMapper mOurMapper;

        public Mapper(IOurMapper ourMapper)
        {
            mOurMapper = ourMapper;
        }

        public FileModelDto FileModelToDto(FileModel model)
        {
            return mOurMapper.Map<FileModelDto>(model);
        }

        public FileModel DtoToFileModel(FileModelDto dto)
        {
            return mOurMapper.Map<FileModel>(dto);
        }

        public WorkspaceModelDto WorkspaceModelToDto(WorkspaceModel model)
        {
            return mOurMapper.Map<WorkspaceModelDto>(model);
        }
         
        public WorkspaceModel DtoToWorkspaceModel(WorkspaceModelDto dto)
        {
            return mOurMapper.Map<WorkspaceModel>(dto);
        }
    }
}
