import { Component, Input, OnChanges, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { WorkspaceModelDto } from '../../models/workspace/workspace-dto';
import { FileModelDto } from '../../models/file/file-dto';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatDialog } from '@angular/material/dialog';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { environment } from 'src/environments/environment';
import { WorkspaceService } from '../../services/workspace.service';
import { FileUploadDialogComponent } from './file-upload-dialog/file-upload-dialog.component';
import { Mediator } from 'src/app/interfaces/mediator';
import { FileRemoveDialogComponent } from './file-remove-dialog/file-remove-dialog.component';

@Component({
    selector: 'app-workspace',
    templateUrl: './workspace.component.html',
    styleUrls: ['./workspace.component.scss'],
    animations: [
        trigger('detailExpand', [
            state('collapsed', style({ height: '0px', minHeight: '0' })),
            state('expanded', style({ height: '*' })),
            transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
        ]),
    ],
})
export class WorkspaceComponent implements OnInit {
    constructor(private mWorkspaceService: WorkspaceService, public dialog: MatDialog) {}

    public chosenWorkspace: WorkspaceModelDto;
    //public files: FileModelDto[];


    columnsToDisplay = ['fileName', 'contentType', 'lastModificationDate', 'lastModificationBy'];

    @Input() mediator: Mediator;

    dataSource: MatTableDataSource<FileModelDto> = new MatTableDataSource([]);

    @ViewChild(MatPaginator) paginator: MatPaginator;

    @ViewChild(MatSort) sort: MatSort;

    pageSizeOptions = environment.workspacePageSize;

    tableDisplay: { display: string } = {display: 'none'};
    isTableDisplayed = false;

    isDataLoaded = false;

    expandedElement: FileModelDto | null;

    ngOnInit(): void {}

    public setWorkspace(files : FileModelDto[], chosenWorkspace: WorkspaceModelDto)
    {
        this.dataSource.data = files;
        this.chosenWorkspace = chosenWorkspace;
        this.isDataLoaded = true;
            this.tableDisplay = {
                display: this.chosenWorkspace.id != null ? 'block' : 'none',
            };

            this.dataSource.paginator = this.paginator;
            this.dataSource.sort = this.sort;
    }

    // ngOnChanges(changes: SimpleChanges) {
    //     //later a place for ../api/workspaces/ID/files GET method
    //     const workspaceChanges = changes['chosenWorkspace'];
    //     const currentWorkspace: WorkspaceModelDto = workspaceChanges.currentValue;
       
    //     //         {
    //     //             id: '83',
    //     //             groupId: '69',
    //     //             description: 'co do kurwy',
    //     //             fileName: 'lingwistyka',
    //     //             file: 'base64',
    //     //             contentType: 'jpg',
    //     //             lastModificationDate: '01/01/2137',
    //     //             lastModifiedBy: 'Bob',
    //     //         },

    //     this.isDataLoaded = false;

    //     this.mWorkspaceService.getWorkspacesOverview(currentWorkspace.id).subscribe(
    //         (result: FileModelDto[]) => {
    //             //console.log();
    //             //console.log(JSON.stringify(result));
    //             this.files = result;
    //             this.dataSource.data = this.files;
    //             this.isDataLoaded = true;
    //         },
    //         (error) => {
    //             console.log(error);
    //         }
    //     );
    //     //console.log(this.files);

    //     this.tableDisplay = {
    //         display: this.chosenWorkspace.id != null ? 'block' : 'none',
    //     };

    //     this.dataSource.paginator = this.paginator;
    //     this.dataSource.sort = this.sort;
    // }

    applyFilter(event: Event) {
        this.dataSource.filterPredicate = function (data, filter: string): boolean {
            return data.fileName.toLowerCase().includes(filter);
        };
        const filterValue = (event.target as HTMLInputElement).value;
        this.dataSource.filter = filterValue.trim().toLowerCase();

        if (this.dataSource.paginator) {
            this.dataSource.paginator.firstPage();
        }
    }

    openDialog() {
        const dialogRef = this.dialog.open(FileUploadDialogComponent, {
            data: { workspaceId: this.chosenWorkspace.id },
        });
        dialogRef.afterClosed().subscribe((result) => {
            console.log(`Dialog result: ${result}`);
        });
    }

    downloadFile(idOfWorkspace: string, idOfFile: string, fname: string) {
        this.mediator.notify(this,{type:'downloadFile', content: {workspaceId: idOfWorkspace, fileId: idOfFile, filename: fname}});
    }

    openRemoveDialog(wId : string, fId : string, fName : string){
        const dialogRef = this.dialog.open(FileRemoveDialogComponent, {
            data: {filename: fName},
        });
        dialogRef.afterClosed().subscribe((result) =>{
            if(result == 'accept'){
                this.mediator.notify(this, {type:'removeFile', content: {workspaceId: wId, fileId: fId}});
            }
        });
    }
}
