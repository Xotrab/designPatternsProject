using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FileForgeDP.Database.Repositories;
using FileForgeDP.Mappers;
using FileForgeDP.Database.Dto;
using System.Linq;
using System.Collections.Generic;
using FileForgeDP.Facades;

namespace FileForgeDP.Controllers
{
    [Route("api/")]
    [ApiController]
    public class WorkspaceController : ControllerBase
    {

        private readonly WorkspacesFacade mWorkspacesFacade;

        public WorkspaceController(WorkspacesFacade workspacesFacade)
        {
            this.mWorkspacesFacade = workspacesFacade;
      
        }

        // /api/workspaces   -   add workspace
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Route("workspaces")]
        public async Task<IActionResult> PostWorkspaceModel([FromBody] WorkspaceModelDto workspaceModelDto)
        {
            var createdId = mWorkspacesFacade.AddWorkspace(workspaceModelDto);

            return CreatedAtRoute(new { id = createdId }, new { name = workspaceModelDto.Name, id = createdId });
        }

        // /api/workspaces/workspaceId  -   add file to the workspace
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Route("workspaces/{id}")]
        public async Task<IActionResult> PostWorkspaceModel(string id, [FromBody] FileModelDto fileModelDto)
        {
            // Strange but necessary
            fileModelDto.GroupId = id;

            var createdId = mWorkspacesFacade.AddFileToWorkspace(fileModelDto);

            return CreatedAtRoute(new { id = createdId }, new { fileName = fileModelDto.FileName, id = createdId, groupId = fileModelDto.GroupId });
        }

        [HttpGet]
        [Route("workspaces/{workspaceId}/files")]
        public  ActionResult<List<FileModelDto>> GetAllWorkspaceFiles(string workspaceId)
        {
            return mWorkspacesFacade.GetWorkspaceFiles(workspaceId);
        }
        // /api/workspaces/workspaceId/fileId   - get the file(all its content) from the workspace
        [HttpGet]  
        [Route("workspaces/{workspaceId}/files/{fileId}")]
        public async Task<ActionResult<FileModelDto>> GetFileModel(string workspaceId, string fileId)
        {
            return mWorkspacesFacade.GetFileFromWorkspace(workspaceId, fileId);
        }
        // /api/workspaces/workspaceId  -   get the workspace
        [HttpGet]
        [Route("workspaces/{id}")]
        public async Task<ActionResult<WorkspaceModelDto>> GetWorkspaceModel(string id)
        {
            return mWorkspacesFacade.GetWorkspace(id);
        }

        // /api/workspaces/workspaceId  -   delete the workspace and all it's files
        [HttpDelete]
        [Route("workspaces/{id}")]
        public async Task<ActionResult<WorkspaceModelDto>> DeleteWorkspaceModel(string id)
        {
            mWorkspacesFacade.DeleteWorkspace(id);
            return NoContent();
        }

        // /api/workspaces/workspaceId/fileId  - remove the file from the workspace
        [HttpDelete]
        [Route("workspaces/{workspaceId}/{fileId}")]
        public async Task<ActionResult<WorkspaceModelDto>> DeleteWorkspaceModel(string workspaceId, string fileId)
        {
            mWorkspacesFacade.RemoveFileFromWorkspace(workspaceId, fileId);
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

            mWorkspacesFacade.UpdateWorkspace(id, workspaceModelDto);

            return NoContent();
        }
        // /api/workspaces/workspaceid/fileid
        [HttpPut]
        [Route("workspaces/{workspaceId}/{fileId}")]
        public async Task<ActionResult<WorkspaceModelDto>> PutWorkspaceModelFile(string workspaceId, string fileId, [FromBody] FileModelDto fileModelDto)
        {
            if (fileId != fileModelDto.Id || workspaceId != fileModelDto.GroupId)
            {
                return BadRequest();
            };

            mWorkspacesFacade.UpdateWorkspaceFile(workspaceId, fileId, fileModelDto);

            return NoContent();
        }

    }
}
