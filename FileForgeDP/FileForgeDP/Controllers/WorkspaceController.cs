using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FileForgeDP.Database.Repositories;
using FileForgeDP.Mappers;
using FileForgeDP.Database.Dto;

namespace FileForgeDP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkspaceController : ControllerBase
    {

        private readonly WorkspaceRepository mWorkspaceService;

        private readonly WorkspaceModelMapper mWorkspaceModelMapper;
        private readonly FileModelMapper mFileModelMapper;

        public WorkspaceController(WorkspaceRepository workspaceService, WorkspaceModelMapper workspaceModelMapper, FileModelMapper fileModelMapper)
        {
            mWorkspaceService = workspaceService;
            mWorkspaceModelMapper = workspaceModelMapper;
            mFileModelMapper = fileModelMapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Route("add")]
        public async Task<IActionResult> PostWorkspaceModel([FromForm] WorkspaceModelDto workspaceModelDto)
        {

            var workspaceToSave = mWorkspaceModelMapper.DtoToWorkspaceModel(workspaceModelDto);

            var createdId = mWorkspaceService.Create(workspaceToSave);

            return CreatedAtRoute(new { id = createdId }, new { workspaceName = workspaceModelDto.Name, id = createdId});
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Route("{id}")]
        public async Task<IActionResult> PostWorkspaceModel(string id, [FromBody] FileModelDto fileModelDto)
        {
           
            fileModelDto.GroupId = id;
            var fileModel = mFileModelMapper.DtoToFileModel(fileModelDto);
            var createdId = mWorkspaceService.Create(fileModel);

            return CreatedAtRoute(new { id = createdId }, new { fileName = fileModelDto.FileName, id = createdId, groupId = fileModelDto.GroupId });
        }

        [HttpGet("{id}")]
        //[Route("check")]
        public async Task<ActionResult<WorkspaceModelDto>> GetWorkspaceModel(string id)
        {
            var result = mWorkspaceService.Get(id);

            return mWorkspaceModelMapper.WorkspaceModelToDto(result);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<WorkspaceModelDto>> DeleteWorkspaceModel(string id)
        {
            mWorkspaceService.Remove(id);
            return NoContent();
        }
    }
}
