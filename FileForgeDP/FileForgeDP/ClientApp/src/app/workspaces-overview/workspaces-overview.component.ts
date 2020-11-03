import { Component, Input, OnInit } from '@angular/core';
import { WorkspaceModelDto } from '../models/workspace/workspace-dto';

@Component({
    selector: 'app-workspaces-overview',
    templateUrl: './workspaces-overview.component.html',
    styleUrls: ['./workspaces-overview.component.scss'],
})
export class WorkspacesOverviewComponent implements OnInit {
    constructor() {}
    public currentWorkspace: WorkspaceModelDto = null;

    ngOnInit(): void {}
    public onWorkspaceChange(event: WorkspaceModelDto) {
        this.currentWorkspace = event;
    }
}
