import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { Mediator } from '../interfaces/mediator';
import { FileModelDto } from '../models/file/file-dto';
import { WorkspaceModelDto } from '../models/workspace/workspace-dto';
import { WorkspaceService } from '../services/workspace.service';
import { WorkspaceComponent } from './workspace-component/workspace.component';
import { WorkspaceSidebarComponent } from './workspace-sidebar/workspace-sidebar.component';

@Component({
    selector: 'app-workspaces-overview',
    templateUrl: './workspaces-overview.component.html',
    styleUrls: ['./workspaces-overview.component.scss'],
})
export class WorkspacesOverviewComponent implements OnInit, Mediator {
    constructor(private mWorkspaceService: WorkspaceService) {}

    @ViewChild(WorkspaceComponent) workspace: WorkspaceComponent;
    @ViewChild(WorkspaceSidebarComponent) sidebar: WorkspaceSidebarComponent;

    notify(sender: object, event: { type: String; content: any }): void {
        switch (sender) {

            case this.sidebar:
                switch (event.type) {
                    case 'loadWorkspaces':
                        throw new Error("XD");
                    break;
                    case 'workspaceChange':
                        this.currentWorkspace = event.content;
                        this.mWorkspaceService.getWorkspacesOverview(this.currentWorkspace.id).subscribe(
                            (result: FileModelDto[]) => {
                                this.workspace.setWorkspace(result, this.currentWorkspace);
                            },
                            (error) => {
                                throw new Error(error);
                            }
                        );
                        break;
                    default:
                        throw new Error('Unexpected sidebar operation');
                }
            break;

            case this.workspace:
                switch (event.type) {
                    case 'downloadFile':
                       var workspaceId = event.content.workspaceId;
                       var fileId = event.content.fileId;
                       var filename = event.content.filename;
                       
                        this.mWorkspaceService.downloadWorkspaceFile(workspaceId, fileId).subscribe(
                            (response: any) => {
                                let dataType = response.type;
                                let binaryData = [];
                                binaryData.push(response);
                                let downloadLink = document.createElement('a');
                                downloadLink.href = window.URL.createObjectURL(
                                    new Blob(binaryData, { type: dataType })
                                );
                                if (filename) downloadLink.setAttribute('download', filename);
                                document.body.appendChild(downloadLink);
                                downloadLink.click();
                            },
                            (error) => { throw new Error(error); }
                        );    
                    break;
                    default:
                        throw new Error('Unexpected sidebar operation');
                }
        }
    }
    public currentWorkspace: WorkspaceModelDto = { id: null, name: '' };

    ngOnInit(): void {}
    public onWorkspaceChange(event: WorkspaceModelDto) {
        this.currentWorkspace = event;
    }
}
