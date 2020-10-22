namespace FileForgeDP.Mappers
{
    using AutoMapper;
    using FileForgeDP.Database.Dto;
    using FileForgeDP.Database.Models;
    using FileForgeDP.Extensions;

    /// <summary>
    /// Defines the <see cref="FileModelMapper" />.
    /// </summary>
    public class FileModelMapper
    {
        /// <summary>
        /// Defines the mMapper.
        /// </summary>
        private readonly IMapper mMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileModelMapper"/> class.
        /// </summary>
        public FileModelMapper()
        {
            mMapper = new MapperConfiguration(x =>
           {
               x.CreateMap<FileModelDto, FileModel>()
                .ForMember(model => model.ContentType, map => map.MapFrom(dto => dto.File == null ? string.Empty : dto.File.ContentType))
                .ForMember(model => model.File, map => map.MapFrom(dto => dto.File.ToBytes().Result))
                .ReverseMap()
                .ForMember(dto => dto.File, map => map.MapFrom(model => model.File.ToIFormFIle(model.FileName)));
           }).CreateMapper();
        }

        /// <summary>
        /// The FileModelToDto.
        /// </summary>
        /// <param name="fileModel">The fileModel<see cref="FileModel"/>.</param>
        /// <returns>The <see cref="FileModelDto"/>.</returns>
        public FileModelDto FileModelToDto(FileModel fileModel)
        {
            return mMapper.Map<FileModelDto>(fileModel);
        }

        /// <summary>
        /// The DtoToFileModel.
        /// </summary>
        /// <param name="fileModelDto">The fileModelDto<see cref="FileModelDto"/>.</param>
        /// <returns>The <see cref="FileModel"/>.</returns>
        public FileModel DtoToFileModel(FileModelDto fileModelDto)
        {
            return mMapper.Map<FileModel>(fileModelDto);
        }
    }
}
