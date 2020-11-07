import { Component, EventEmitter, Output, OnInit } from '@angular/core';
import { WorkspaceService } from '../../services/workspace.service';
import { WorkspaceModelDto } from '../../models/workspace/workspace-dto';

@Component({
    selector: 'app-workspace-sidebar',
    templateUrl: './workspace-sidebar.component.html',
    styleUrls: ['./workspace-sidebar.component.scss'],
})
export class WorkspaceSidebarComponent implements OnInit {
    @Output('workspaceChangeEvent') workspaceChangeEvent = new EventEmitter<WorkspaceModelDto>();

    constructor(private mWorkspaceService: WorkspaceService) {}
    workspaces: WorkspaceModelDto[];
    highlighted = null;

    onClick(i) {
        this.highlighted = i;
        this.workspaceChangeEvent.emit(this.workspaces[i]);
    }
    ngOnInit(): void {
        this.mWorkspaceService.getWorkspacesOverview('5f99cd14985e3f043152b51b').subscribe(
            (result) => {
                console.log();
                console.log(JSON.stringify(result));
            },
            (error) => {
                console.log(error);
            }
            
        );
        this.workspaces = [
            { id: '1', name: 'dupawka' },
            { id: '2137', name: 'Watykan' },
            { id: '69', name: 'Disawka' },
        ];
    }
}
