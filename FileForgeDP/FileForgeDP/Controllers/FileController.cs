namespace FileForgeDP.Controllers
{
    using FileForgeDP.Database.Dto;
    using FileForgeDP.Database.Repositories;
    using FileForgeDP.Mappers;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="FileController" />.
    /// </summary>
    [ApiController]
    [Route("api/")]
    public class FileController : ControllerBase
    {
        /// <summary>
        /// Defines the mFileService.
        /// </summary>
        private readonly FileRepository mFileService;

        /// <summary>
        /// Defines the mFileModelMapper.
        /// </summary>
        private readonly FileModelMapper mFileModelMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileController"/> class.
        /// </summary>
        /// <param name="fileService">The fileService<see cref="FileRepository"/>.</param>
        /// <param name="fileModelMapper">The fileModelMapper<see cref="FileModelMapper"/>.</param>
        public FileController(FileRepository fileService, FileModelMapper fileModelMapper)
        {
            mFileService = fileService;
            mFileModelMapper = fileModelMapper;
        }

        /// <summary>
        /// The PostFileModel.
        /// </summary>
        /// <param name="fileModelDto">The fileModelDto<see cref="FileModelDto"/>.</param>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Route("files")]
        public async Task<IActionResult> PostFileModel([FromForm] FileModelDto fileModelDto)
        {

            var fileToSave = mFileModelMapper.DtoToFileModel(fileModelDto);

            var createdId = mFileService.Create(fileToSave);

            return CreatedAtRoute(new { id = createdId }, new { fileName = fileModelDto.FileName, id = createdId, groupId = fileModelDto.GroupId });
        }

        /// <summary>
        /// The GetFileModel.
        /// </summary>
        /// <param name="id">The id<see cref="string"/>.</param>
        /// <returns>The <see cref="Task{CreatedResult}"/>.</returns>
        //[HttpGet]
        //[Route("files")]
        //public async Task<CreatedResult> GetFileModel(string id)
        //{
        //    var result = mFileService.Get(id);

        //    return new CreatedResult("xd de nie mam jeszcze api", result);
        //}
    }
}
