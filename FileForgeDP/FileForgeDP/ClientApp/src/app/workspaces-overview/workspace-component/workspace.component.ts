import { Component, Input, OnInit } from '@angular/core';
import { WorkspaceModelDto } from '../../models/workspace/workspace-dto';

@Component({
    selector: 'app-workspace',
    templateUrl: './workspace.component.html',
    styleUrls: ['./workspace.component.scss'],
})
export class WorkspaceComponent implements OnInit {
    constructor() {}
    @Input() public chosenWorkspace: WorkspaceModelDto;
    ngOnInit(): void {}
}
