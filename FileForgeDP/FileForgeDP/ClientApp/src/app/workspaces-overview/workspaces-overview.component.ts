import { Component, OnInit } from '@angular/core';
import { WorkspaceService } from '../services/workspace.service';

@Component({
    selector: 'app-workspaces-overview',
    templateUrl: './workspaces-overview.component.html',
    styleUrls: ['./workspaces-overview.component.scss'],
})
export class WorkspacesOverviewComponent implements OnInit {
    constructor(private konradKurwaJest: WorkspaceService) {}

    ngOnInit(): void {
        this.konradKurwaJest.getWorkspacesOverview('5f99cd14985e3f043152b51b').subscribe(
            (result) => {
                console.log('Konrad to ziomek');
                console.log(JSON.stringify(result));
            },
            (error) => {
                console.log(error);
            },
            () => {
                console.log('Konrad jednak prawdomówny');
            }
        );
    }
}
