﻿namespace FileForgeDP.Controllers
{
    using FileForgeDP.Database.Dto;
    using FileForgeDP.Extensions;
    using FileForgeDP.Facades;
    using FileForgeDP.Loggers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    [Authorize(Roles = "Administrator")]
    [Route("api/")]
    [ApiController]
    public class WorkspaceController : ControllerBase
    {
        private readonly WorkspacesFacade mWorkspacesFacade;
        private readonly ILogger mAuditLogger;
   
        public WorkspaceController(WorkspacesFacade workspacesFacade, ILogger logger)
        {
            mWorkspacesFacade = workspacesFacade;
            mAuditLogger = logger;
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
            mAuditLogger.Debug(RetrieveUsername(), ActionEnum.GET, ControllerContext.ActionDescriptor.ActionName, HttpStatusCode.OK);
            return mWorkspacesFacade.GetWorkspaceFiles(workspaceId);
        }
  
        [HttpGet]  
        [Route("workspaces/{workspaceId}/files/{fileId}")]
        public IActionResult GetFileModelAsync(string workspaceId, string fileId)
        {
            var result =  mWorkspacesFacade.GetFileFromWorkspace(workspaceId, fileId);

            mAuditLogger.Debug(User?.Claims.FirstOrDefault(x => x.Type == "name")?.Value ?? "undefined", ActionEnum.GET, ControllerContext.ActionDescriptor.ActionName, result != null ? HttpStatusCode.OK : HttpStatusCode.BadRequest);

            return File(result.FileBytes, result.ContentType , result.FileName);
        }
    
        [HttpGet]
        [Route("workspaces/{id}")]
        public async Task<ActionResult<WorkspaceModelDto>> GetWorkspaceModel(string id)
        {
            var result = mWorkspacesFacade.GetWorkspace(id);
            
            mAuditLogger.Debug(RetrieveUsername() , ActionEnum.GET, ControllerContext.ActionDescriptor.ActionName, result != null ? HttpStatusCode.OK : HttpStatusCode.BadRequest );
            
            return result;
        }

        [HttpDelete]
        [Route("workspaces/{id}")]
        public async Task<ActionResult<WorkspaceModelDto>> DeleteWorkspaceModel(string id)
        {
            mWorkspacesFacade.DeleteWorkspace(id);
            mAuditLogger.Debug(RetrieveUsername(), ActionEnum.DELETE, ControllerContext.ActionDescriptor.ActionName, HttpStatusCode.NoContent);
            return NoContent();
        }

        [HttpDelete]
        [Route("workspaces/{workspaceId}/files/{fileId}")]
        public async Task<ActionResult<WorkspaceModelDto>> DeleteWorkspaceModel(string workspaceId, string fileId)
        {   
            mWorkspacesFacade.RemoveFileFromWorkspace(workspaceId, fileId);
            mAuditLogger.Debug(RetrieveUsername(), ActionEnum.DELETE, ControllerContext.ActionDescriptor.ActionName, HttpStatusCode.NoContent);
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
            mAuditLogger.Debug(RetrieveUsername(), ActionEnum.DELETE, ControllerContext.ActionDescriptor.ActionName, HttpStatusCode.OK);
            mWorkspacesFacade.UpdateWorkspace(id, workspaceModelDto);

            return NoContent();
        }

        [HttpPut]
        [Route("workspaces/{workspaceId}/files/{fileId}")]
        public async Task<ActionResult<WorkspaceModelDto>> PutWorkspaceModelFile(string workspaceId, string fileId, [FromBody] FileModelDto fileModelDto)
        {
            if (fileId != fileModelDto.Id || workspaceId != fileModelDto.GroupId)
            {
                return BadRequest();
            };

            mWorkspacesFacade.UpdateWorkspaceFile(workspaceId, fileId, fileModelDto);

            return NoContent();
        }

        private string RetrieveUsername()
        {
            return User?.Claims.FirstOrDefault(x => x.Type == "name")?.Value ?? "undefined";
        }
    }
}
