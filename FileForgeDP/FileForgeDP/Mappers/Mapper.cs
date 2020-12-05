using System;
using System.Collections.Generic;
using FileForgeDP.Database.Dto;
using FileForgeDP.Database.Models;

namespace FileForgeDP
{
    public class Mapper
    {
        private readonly IOurMapper mOurMapper;

        public Mapper()
        {
            mOurMapper = new OurMapper(new Dictionary<Type, Dictionary<string, Func<object, object>>>
            {
                {
                    typeof(FileModelDto),
                    new Dictionary<string, Func<object, object>>
                    {
                        { nameof(FileModelDto.FileBytes), (x) => (x as FileModel).File },
                        { nameof(FileModelDto.File), (x) => null }
                    }
                },
                {
                    typeof(FileModel),
                    new Dictionary<string, Func<object, object>>
                    {
                        { nameof(FileModel.ContentType), (x) => (x as FileModelDto).File == null || (x as FileModelDto).File.ContentType == null ? (x as FileModelDto).ContentType : (x as FileModelDto).File.ContentType },
                        { nameof(FileModel.File), (x) => (x as FileModelDto).FileBytes }
                    }
                }
            });
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
