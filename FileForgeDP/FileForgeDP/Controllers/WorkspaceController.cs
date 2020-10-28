using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FileForgeDP.Database.Repositories;
using FileForgeDP.Mappers;
using FileForgeDP.Database.Dto;

namespace FileForgeDP.Controllers
{
    [Route("api/")]
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

        // /api/workspaces   -   add workspace
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Route("workspaces")]
        public async Task<IActionResult> PostWorkspaceModel([FromBody] WorkspaceModelDto workspaceModelDto)
        {

            var workspaceToSave = mWorkspaceModelMapper.DtoToWorkspaceModel(workspaceModelDto);

            var createdId = mWorkspaceService.Create(workspaceToSave);

            return CreatedAtRoute(new { id = createdId }, new { name = workspaceModelDto.Name, id = createdId });
        }

        // /api/workspaces/workspaceId  -   add file to the workspace
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Route("workspaces/{id}")]
        public async Task<IActionResult> PostWorkspaceModel(string id, [FromBody] FileModelDto fileModelDto)
        {

            fileModelDto.GroupId = id;
            var fileModel = mFileModelMapper.DtoToFileModel(fileModelDto);
            var createdId = mWorkspaceService.Create(fileModel);

            return CreatedAtRoute(new { id = createdId }, new { fileName = fileModelDto.FileName, id = createdId, groupId = fileModelDto.GroupId });
        }

        // /api/workspaces/workspaceId  -   get the workspace
        [HttpGet]
        [Route("workspaces/{id}")]
        public async Task<ActionResult<WorkspaceModelDto>> GetWorkspaceModel(string id)
        {
            var result = mWorkspaceService.Get(id);

            return mWorkspaceModelMapper.WorkspaceModelToDto(result);
        }

        // /api/workspaces/workspaceId/fileId   - get the file(all its content) from the workspace
        [HttpGet]   // TODO
        [Route("workspaces/{workspaceId}/{fileId}")]
        public async Task<ActionResult<FileModelDto>> GetWorkspaceModel(string workspaceId, string fileId)
        {
            var result = mWorkspaceService.GetFile(workspaceId,fileId);

            return mFileModelMapper.FileModelToDto(result);
        }

        // /api/workspaces/workspaceId  -   delete the workspace and all it's files
        [HttpDelete]
        [Route("workspaces/{id}")]
        public async Task<ActionResult<WorkspaceModelDto>> DeleteWorkspaceModel(string id)
        {
            mWorkspaceService.Remove(id);
            return NoContent();
        }

        // /api/workspaces/workspaceId/fileId  - remove the file from the workspace
        [HttpDelete]
        [Route("workspaces/{workspaceId}/{fileId}")]
        public async Task<ActionResult<WorkspaceModelDto>> DeleteWorkspaceModel(string workspaceId, string fileId)
        {
            mWorkspaceService.RemoveOne(workspaceId, fileId);
            return NoContent();
        }

        // /api/workspaces/workspaceId   - update the workspace
        [HttpPut]
        [Route("workspaces/{id}")]
        public async Task<ActionResult<WorkspaceModelDto>> PutWorkspaceModel(string id, [FromForm] WorkspaceModelDto workspaceModelDto)
        {
            if (id != workspaceModelDto.Id)
            {
                return BadRequest();
            }
            var workspaceModel = mWorkspaceModelMapper.DtoToWorkspaceModel(workspaceModelDto);

            mWorkspaceService.Update(id, workspaceModel);

            return NoContent();
        }

    }
}
