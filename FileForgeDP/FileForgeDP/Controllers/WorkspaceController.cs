namespace FileForgeDP.Controllers
{
    using FileForgeDP.Database.Dto;
    using FileForgeDP.Extensions;
    using FileForgeDP.Facades;
    using FileForgeDP.Loggers;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

   
    [Route("api/")]
    [ApiController]
    public class WorkspaceController : ControllerBase
    {
      
        private readonly WorkspacesFacade mWorkspacesFacade;
        private readonly ILogger mAuditLogger;
   
        public WorkspaceController(WorkspacesFacade workspacesFacade, ILogger logger)
        {
            this.mWorkspacesFacade = workspacesFacade;
            //Bad Pratice, but this way we avoid messing up Log() parameters 
            this.mAuditLogger = logger;
        }
    
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Route("workspaces")]
        public async Task<IActionResult> PostWorkspaceModel([FromBody] WorkspaceModelDto workspaceModelDto)
        {
            var createdId = mWorkspacesFacade.AddWorkspace(workspaceModelDto);

            return CreatedAtRoute(new { id = createdId }, new { name = workspaceModelDto.Name, id = createdId });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Route("workspaces/{id}")]
        public async Task<IActionResult> PostWorkspaceModel(string id, [FromForm] FileModelDto fileModelDto)
        {
            // Strange but necessary
            fileModelDto.GroupId = id;

            fileModelDto.FileBytes = await fileModelDto.File.ToBytes();
            var createdId = mWorkspacesFacade.AddFileToWorkspace(fileModelDto);

            return CreatedAtRoute(new { id = createdId }, new { fileName = fileModelDto.FileName, id = createdId, groupId = fileModelDto.GroupId });
        }

        [HttpGet]
        [Route("workspaces/{workspaceId}/files")]
        public  ActionResult<List<FileOverviewDto>> GetAllWorkspaceFiles(string workspaceId)
        {
            return mWorkspacesFacade.GetWorkspaceFiles(workspaceId);
        }
  
        [HttpGet]  
        [Route("workspaces/{workspaceId}/files/{fileId}")]
        public IActionResult GetFileModelAsync(string workspaceId, string fileId)
        {
            var result1 =  mWorkspacesFacade.GetFileFromWorkspace(workspaceId, fileId);
           
         
            return File(result1.FileBytes, result1.ContentType , result1.FileName);
        }
    
        [HttpGet]
        [Route("workspaces/{id}")]
        public async Task<ActionResult<WorkspaceModelDto>> GetWorkspaceModel(string id)
        {
            var result = mWorkspacesFacade.GetWorkspace(id);
            
            mAuditLogger.Debug("Patryk .Net Dev have to provide user somehow", ActionEnum.GET, ControllerContext.ActionDescriptor.ActionName, result != null ? "200" : "400" );
            
            return result;
        }

        [HttpDelete]
        [Route("workspaces/{id}")]
        public async Task<ActionResult<WorkspaceModelDto>> DeleteWorkspaceModel(string id)
        {
            mWorkspacesFacade.DeleteWorkspace(id);
            return NoContent();
        }

        [HttpDelete]
        [Route("workspaces/{workspaceId}/{fileId}")]
        public async Task<ActionResult<WorkspaceModelDto>> DeleteWorkspaceModel(string workspaceId, string fileId)
        {
            mWorkspacesFacade.RemoveFileFromWorkspace(workspaceId, fileId);
            return NoContent();
        }

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
