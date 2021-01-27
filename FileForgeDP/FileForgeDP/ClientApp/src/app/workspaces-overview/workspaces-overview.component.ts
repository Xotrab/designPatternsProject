import { formatDate } from '@angular/common';
import { HttpEventType } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { map } from 'rxjs/operators';
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
    constructor(private oauthService: OAuthService, private mWorkspaceService: WorkspaceService) {}

    @ViewChild(WorkspaceComponent) workspace: WorkspaceComponent;
    @ViewChild(WorkspaceSidebarComponent) sidebar: WorkspaceSidebarComponent;

    uploaded : number = 0;

    workspaceChangeHandler(event){
        this.currentWorkspace = event.content;
        this.mWorkspaceService
            .getWorkspaceFiles(this.currentWorkspace.id)
            .subscribe(
                (result: FileModelDto[]) => {
                    this.workspace.setWorkspace(result, this.currentWorkspace);
                },
                (error) => {
                    throw new Error(error);
                }
            );
    }

    uploadFilesHandler(event){
        var files = event.content.files;
        var progress = event.content.progressList;
        var isDone = event.content.doneFlag;
        var count = 0;

        files.forEach((file) => {
            progress.push(0);
            this.uploadFile(count++, file, progress, isDone);
        });

    }

    downloadFileHandler(event){
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
            (error) => {
                throw new Error(error);
            }
        );
    }

    removeFileHandler(event){
        var workspaceId = event.content.workspaceId;
        var fileId = event.content.fileId;

        this.mWorkspaceService.removeWorkspaceFile(workspaceId, fileId).subscribe(
            (response: any) => {
                this.mWorkspaceService
                    .getWorkspaceFiles(this.currentWorkspace.id)
                    .subscribe(
                        (result: FileModelDto[]) => {
                            this.workspace.setWorkspace(
                                result,
                                this.currentWorkspace
                            );
                        },
                        (error) => {
                            throw new Error(error);
                        }
                    );
            },
            (error) => {
                throw new Error(error);
            }
        );
    }

    updateFileHandler(event){
        var update: FileModelDto = {
            id: event.content.fileId,
            groupId: event.content.workspaceId,
            description: event.content.description,
            fileName: event.content.filename,
            file: null,
            contentType: null,
            lastModificationDate: String(
                formatDate(new Date(), 'dd/MM/yyyy', 'en')
            ),
            lastModifiedBy: (<any>this.oauthService.getIdentityClaims()).preferred_username,
        };
        this.mWorkspaceService
            .updateWorkspaceFile(update.groupId, update.id, update)
            .subscribe(
                (response: any) => {
                    this.mWorkspaceService
                        .getWorkspaceFiles(this.currentWorkspace.id)
                        .subscribe(
                            (result: FileModelDto[]) => {
                                this.workspace.setWorkspace(
                                    result,
                                    this.currentWorkspace
                                );
                            },
                            (error) => {
                                throw new Error(error);
                            }
                        );
                },
                (error) => {
                    throw new Error(error);
                }
            );
    }

    notify(sender: object, event: { type: String; content: any }): void {
        switch (sender) {
            case this.sidebar:
                switch (event.type) {
                    
                    case 'workspaceChange':
                        this.workspaceChangeHandler(event);
                        break;
                    default:
                        throw new Error('Unknown sidebar operation');
                }
                break;

            case this.workspace:
                switch (event.type) {
                    case 'uploadFiles':
                        this.uploadFilesHandler(event);
                        break;

                    case 'downloadFile':
                        this.downloadFileHandler(event);
                        break;
                    case 'removeFile':
                        this.removeFileHandler(event);
                        break;
                    case 'updateFile':
                        this.updateFileHandler(event);
                        break;
                    default:
                        throw new Error('Unknown workspace operation');
                }
        }
    }
    public currentWorkspace: WorkspaceModelDto = { id: null, name: '' };

    ngOnInit(): void {}
    public onWorkspaceChange(event: WorkspaceModelDto) {
        this.currentWorkspace = event;
    }
    
    uploadFile(fileNumber: number, file: FileModelDto, progress : Array<number>, isDone: {value : boolean}) {
        const formatData = new FormData();
        formatData.append('file', file.file);
        formatData.append('fileName', file.file.name);
        formatData.append('lastModificationDate', <string>file.lastModificationDate);
        formatData.append('description', <string>file.description == "" ? "(no description)" : <string>file.description);
        formatData.append('lastModifiedBy', (<any>this.oauthService.getIdentityClaims()).preferred_username);

        this.mWorkspaceService
            .uploadWorkspaceFile(file.groupId, formatData)
            .pipe(
                map((event) => {
                    switch (event.type) {
                        case HttpEventType.UploadProgress:
                            progress[fileNumber] = Math.round(
                                (event.loaded * 100) / event.total
                            );
                            break;
                        case HttpEventType.Response:
                            return event;
                    }
                })
            )
            .subscribe((event: any) => {
                if (typeof event === 'object') {
                    if(event.body.id != null)
                    {
                        this.uploaded += 1;
                        if(this.uploaded==progress.length)
                        {
                            this.uploaded = 0;
                            this.mWorkspaceService
                                .getWorkspaceFiles(this.currentWorkspace.id)
                                .subscribe(
                                    (result: FileModelDto[]) => {
                                        this.workspace.setWorkspace(result, this.currentWorkspace);
                                        isDone.value = true;
                                    },
                                    (error) => {
                                        throw new Error(error);
                                    }
                                );
                            isDone.value = true;
                        }
                    }
                    
                }
            });
    }
}
