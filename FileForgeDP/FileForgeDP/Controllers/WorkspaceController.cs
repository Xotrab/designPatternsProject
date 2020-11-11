using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FileForgeDP.Database.Repositories;
using FileForgeDP.Mappers;
using FileForgeDP.Database.Dto;
using System.Linq;
using System.Collections.Generic;

namespace FileForgeDP.Controllers
{
    [Route("api/")]
    [ApiController]
    public class WorkspaceController : ControllerBase
    {

        private readonly WorkspaceRepository mWorkspaceRepository;
        private readonly FileRepository mFileRepository;

        private readonly WorkspaceModelMapper mWorkspaceModelMapper;
        private readonly FileModelMapper mFileModelMapper;

        public WorkspaceController(WorkspaceRepository workspaceService, FileRepository fileRepository, WorkspaceModelMapper workspaceModelMapper, FileModelMapper fileModelMapper)
        {
            mWorkspaceRepository = workspaceService;
            mWorkspaceModelMapper = workspaceModelMapper;
            mFileRepository = fileRepository;
            mFileModelMapper = fileModelMapper;
        }

        // /api/workspaces   -   add workspace
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Route("workspaces")]
        public async Task<IActionResult> PostWorkspaceModel([FromBody] WorkspaceModelDto workspaceModelDto)
        {

            var workspaceToSave = mWorkspaceModelMapper.DtoToWorkspaceModel(workspaceModelDto);

            var createdId = mWorkspaceRepository.Create(workspaceToSave);

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
            var createdId = mWorkspaceRepository.Create(fileModel);

            return CreatedAtRoute(new { id = createdId }, new { fileName = fileModelDto.FileName, id = createdId, groupId = fileModelDto.GroupId });
        }

        [HttpGet]
        [Route("workspaces/{workspaceId}/files")]
        public  ActionResult<List<FileModelDto>> GetAllWorkspaceFiles(string workspaceId)
        {
            var result = mFileRepository.Get(workspaceId);

            return result.Select(x => mFileModelMapper.FileModelToDto(x)).ToList();
        }
        // /api/workspaces/workspaceId/fileId   - get the file(all its content) from the workspace
        [HttpGet]  
        [Route("workspaces/{workspaceId}/files/{fileId}")]
        public async Task<ActionResult<FileModelDto>> GetFileModel(string workspaceId, string fileId)
        {
            var result = mWorkspaceRepository.GetFile(workspaceId,fileId);

            return mFileModelMapper.FileModelToDto(result);
        }
        // /api/workspaces/workspaceId  -   get the workspace
        [HttpGet]
        [Route("workspaces/{id}")]
        public async Task<ActionResult<WorkspaceModelDto>> GetWorkspaceModel(string id)
        {
            var result = mWorkspaceRepository.Get(id);

            return mWorkspaceModelMapper.WorkspaceModelToDto(result);
        }

        // /api/workspaces/workspaceId  -   delete the workspace and all it's files
        [HttpDelete]
        [Route("workspaces/{id}")]
        public async Task<ActionResult<WorkspaceModelDto>> DeleteWorkspaceModel(string id)
        {
            mWorkspaceRepository.Remove(id);
            return NoContent();
        }

        // /api/workspaces/workspaceId/fileId  - remove the file from the workspace
        [HttpDelete]
        [Route("workspaces/{workspaceId}/{fileId}")]
        public async Task<ActionResult<WorkspaceModelDto>> DeleteWorkspaceModel(string workspaceId, string fileId)
        {
            mWorkspaceRepository.RemoveOne(workspaceId, fileId);
            return NoContent();
        }

        // /api/workspaces/workspaceId   - update the workspace
        [HttpPut]
        [Route("workspaces/{id}")]
        public async Task<ActionResult<WorkspaceModelDto>> PutWorkspaceModel(string id, [FromBody] WorkspaceModelDto workspaceModelDto)
        {
            if (id != workspaceModelDto.Id)
            {
                return BadRequest();
            }
            var workspaceModel = mWorkspaceModelMapper.DtoToWorkspaceModel(workspaceModelDto);

            mWorkspaceRepository.Update(id, workspaceModel);

            return NoContent();
        }
        // /api/workspaces/workspaceid/fileid
        [HttpPut]
        [Route("workspaces/{workspaceId}/{fileId}")]
        public async Task<ActionResult<WorkspaceModelDto>> PutWorkspaceModel(string workspaceId, string fileId, [FromBody] FileModelDto fileModelDto)
        {
            if (fileId != fileModelDto.Id || workspaceId != fileModelDto.GroupId)
            {
                return BadRequest();
            }
            var fileModel = mFileModelMapper.DtoToFileModel(fileModelDto);

            mFileRepository.Update(fileId, fileModel);

            return NoContent();
        }

    }
}
