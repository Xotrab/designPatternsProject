using AutoMapper;
using FileForgeDP.Database.Dto;
using FileForgeDP.Database.Models;

namespace FileForgeDP.Mappers
{
    public class WorkspaceModelMapper
    {
        private readonly IMapper mMapper;

        public WorkspaceModelMapper()
        {
            mMapper = new MapperConfiguration(x =>
            {
                x.CreateMap<WorkspaceModel, WorkspaceModelDto>()
                    .ForMember(model => model.Id, map => map.MapFrom(dto => dto.Id))
                    .ForMember(model => model.Name, map => map.MapFrom(dto => dto.Name))
                    .ReverseMap();
            
            }).CreateMapper();
        }
        public WorkspaceModelDto WorkspaceModelToDto(WorkspaceModel model)
        {
            return mMapper.Map<WorkspaceModelDto>(model);
        }
        public WorkspaceModel DtoToWorkspaceModel(WorkspaceModelDto dto)
        {
            return mMapper.Map<WorkspaceModel>(dto);
        }

    }
}
