import { Component, OnInit, Input } from '@angular/core';
import { WorkspaceService } from '../../services/workspace.service';
import { WorkspaceModelDto } from '../../models/workspace/workspace-dto';
import { Mediator } from 'src/app/interfaces/mediator';
import { FileAction } from 'src/app/models/enums/file-action';

@Component({
    selector: 'app-workspace-sidebar',
    templateUrl: './workspace-sidebar.component.html',
    styleUrls: ['./workspace-sidebar.component.scss'],
})
export class WorkspaceSidebarComponent implements OnInit {
    
    constructor(private mWorkspaceService: WorkspaceService) {}
    @Input() mediator: Mediator;
    workspaces: WorkspaceModelDto[];
    highlighted = null;

    onClick(i) {
        this.highlighted = i;
        this.mediator.notify(this, {type: FileAction.WorkspaceChange, content: this.workspaces[i]})
    }
    ngOnInit(): void {      
        this.mWorkspaceService.getUserWorkspaces().subscribe((response: WorkspaceModelDto[]) => {
            this.workspaces = response;
        });
    }
}
