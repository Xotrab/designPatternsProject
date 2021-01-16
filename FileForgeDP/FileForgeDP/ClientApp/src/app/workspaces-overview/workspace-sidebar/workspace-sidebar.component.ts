import { Component, EventEmitter, Output, OnInit, Input } from '@angular/core';
import { WorkspaceService } from '../../services/workspace.service';
import { WorkspaceModelDto } from '../../models/workspace/workspace-dto';
import { Mediator } from 'src/app/interfaces/mediator';

@Component({
    selector: 'app-workspace-sidebar',
    templateUrl: './workspace-sidebar.component.html',
    styleUrls: ['./workspace-sidebar.component.scss'],
})
export class WorkspaceSidebarComponent implements OnInit {
    @Output('workspaceChangeEvent') workspaceChangeEvent = new EventEmitter<WorkspaceModelDto>();

    constructor(private mWorkspaceService: WorkspaceService) {}
    @Input() mediator: Mediator;
    workspaces: WorkspaceModelDto[];
    highlighted = null;

    onClick(i) {
        this.highlighted = i;
        //this.workspaceChangeEvent.emit(this.workspaces[i]);
        this.mediator.notify(this, {type: 'workspaceChange', content: this.workspaces[i]})
    }
    ngOnInit(): void {
        //this.mWorkspaceService.getWorkspaceFiles('5f99cd14985e3f043152b51b').subscribe(
        //    (result) => {
        //        console.log();
        //        console.log(JSON.stringify(result));
        //    },
        //    (error) => {
        //        console.log(error);
        //    }
        //);
        //this.workspaces = [
        //    { id: '5f99cd14985e3f043152b51b', name: 'dupawka' },
        //    { id: '5faaa9d6360c7a4a3128baaa', name: 'Watykan' },
        //    { id: '5faaa9dc360c7a4a3128baab', name: 'Disawka' },
        //];

        this.mWorkspaceService.getUserWorkspaces().subscribe((response: WorkspaceModelDto[]) => {
            this.workspaces = response;
        });
    }
}
