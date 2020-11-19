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
export class WorkspaceComponent implements OnInit, OnChanges {
    constructor(private mWorkspaceService: WorkspaceService, public dialog: MatDialog) {}

    @Input() public chosenWorkspace: WorkspaceModelDto;

    columnsToDisplay = ['fileName', 'contentType', 'lastModificationDate', 'lastModificationBy'];

    files: FileModelDto[];

    dataSource: MatTableDataSource<FileModelDto> = new MatTableDataSource([]);

    @ViewChild(MatPaginator) paginator: MatPaginator;

    @ViewChild(MatSort) sort: MatSort;

    pageSizeOptions = environment.workspacePageSize;

    tableDisplay: { display: string };
    isTableDisplayed = false;

    isDataLoaded = false;

    expandedElement: FileModelDto | null;

    ngOnInit(): void {}

    ngOnChanges(changes: SimpleChanges) {
        //later a place for ../api/workspaces/ID/files GET method
        console.log('jd');
        const workspaceChanges = changes['chosenWorkspace'];
        const currentWorkspace: WorkspaceModelDto = workspaceChanges.currentValue;
        // if (currentWorkspace.id === '1') {
        //     this.files = [
        //         {
        //             id: '4',
        //             groupId: '1',
        //             description: '420 blaze it',
        //             fileName: 'jaranko',
        //             file: 'base64',
        //             contentType: 'jpg',
        //             lastModificationDate: '01/01/2137',
        //             lastModifiedBy: 'Bob',
        //         },
        //         {
        //             id: '2',
        //             groupId: '1',
        //             description:
        //                 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed molestie felis vel leo egestas congue. Ut eu faucibus nulla, non egestas nisi. Aliquam facilisis mattis dolor sed lobortis. Sed eget gravida urna, et vehicula justo. In hac habitasse platea dictumst. Duis vel vulputate quam, vitae porttitor metus. Integer justo quam, semper eu faucibus quis, vulputate vitae nulla. Maecenas scelerisque luctus tellus, sed venenatis neque cursus nec. Quisque in elit et turpis sodales tincidunt nec a risus. Donec vitae dui vel turpis hendrerit sagittis nec vel nunc. Nunc euismod vehicula diam, ac gravida metus auctor at. In a dapibus dui. Aliquam molestie auctor porttitor. Szanować Gierczaka ziomka Polaka, szanownego obywatela. A Bartosz to chuj.',
        //             fileName: 'jaranko2',
        //             file: 'base64',
        //             contentType: 'png',
        //             lastModificationDate: '01/01/2137',
        //             lastModifiedBy: 'Bob',
        //         },
        //         {
        //             id: '0',
        //             groupId: '1',
        //             description: 'xd',
        //             fileName: 'cos',
        //             file: 'base64',
        //             contentType: 'jpg',
        //             lastModificationDate: '01/01/2137',
        //             lastModifiedBy: 'Bob',
        //         },
        //         {
        //             id: '6',
        //             groupId: '1',
        //             description: 'naura',
        //             fileName: 'masno',
        //             file: 'base64',
        //             contentType: 'txt',
        //             lastModificationDate: '01/01/2137',
        //             lastModifiedBy: 'Bob',
        //         },
        //         {
        //             id: '9',
        //             groupId: '1',
        //             description: 'skun mocny',
        //             fileName: 'weedMaster',
        //             file: 'base64',
        //             contentType: 'jpg',
        //             lastModificationDate: '01/01/2137',
        //             lastModifiedBy: 'Bob',
        //         },
        //         {
        //             id: '4',
        //             groupId: '1',
        //             description: 'x420 blaze it',
        //             fileName: 'jaranko',
        //             file: 'base64',
        //             contentType: 'jpg',
        //             lastModificationDate: '01/01/2137',
        //             lastModifiedBy: 'Bob',
        //         },
        //         {
        //             id: '2',
        //             groupId: '1',
        //             description: 'xopis tak o',
        //             fileName: 'jaranko2',
        //             file: 'base64',
        //             contentType: 'png',
        //             lastModificationDate: '01/01/2137',
        //             lastModifiedBy: 'Bob',
        //         },
        //         {
        //             id: '0',
        //             groupId: '1',
        //             description: 'xxd',
        //             fileName: 'cos',
        //             file: 'base64',
        //             contentType: 'jpg',
        //             lastModificationDate: '01/01/2137',
        //             lastModifiedBy: 'Bob',
        //         },
        //         {
        //             id: '6',
        //             groupId: '1',
        //             description: 'xnaura',
        //             fileName: 'masno',
        //             file: 'base64',
        //             contentType: 'txt',
        //             lastModificationDate: '01/01/2137',
        //             lastModifiedBy: 'Bob',
        //         },
        //         {
        //             id: '9',
        //             groupId: '1',
        //             description: 'xskun mocny',
        //             fileName: 'weedMaster',
        //             file: 'base64',
        //             contentType: 'jpg',
        //             lastModificationDate: '01/01/2137',
        //             lastModifiedBy: 'Bob',
        //         },
        //         {
        //             id: '9',
        //             groupId: '1',
        //             description: 'skun mocny',
        //             fileName: 'weedMaster',
        //             file: 'base64',
        //             contentType: 'jpg',
        //             lastModificationDate: '01/01/2137',
        //             lastModifiedBy: 'Bob',
        //         },
        //         {
        //             id: '4',
        //             groupId: '1',
        //             description: 'x420 blaze it',
        //             fileName: 'jaranko',
        //             file: 'base64',
        //             contentType: 'jpg',
        //             lastModificationDate: '01/01/2137',
        //             lastModifiedBy: 'Bob',
        //         },
        //         {
        //             id: '2',
        //             groupId: '1',
        //             description: 'xopis tak o',
        //             fileName: 'jaranko2',
        //             file: 'base64',
        //             contentType: 'png',
        //             lastModificationDate: '01/01/2137',
        //             lastModifiedBy: 'Bob',
        //         },
        //         {
        //             id: '0',
        //             groupId: '1',
        //             description: 'xxd',
        //             fileName: 'cos',
        //             file: 'base64',
        //             contentType: 'jpg',
        //             lastModificationDate: '01/01/2137',
        //             lastModifiedBy: 'Bob',
        //         },
        //         {
        //             id: '6',
        //             groupId: '1',
        //             description: 'xnaura',
        //             fileName: 'masno',
        //             file: 'base64',
        //             contentType: 'txt',
        //             lastModificationDate: '01/01/2137',
        //             lastModifiedBy: 'Bob',
        //         },
        //         {
        //             id: '9',
        //             groupId: '1',
        //             description: 'chujxskun mocny',
        //             fileName: 'weedMaster',
        //             file: 'base64',
        //             contentType: 'jpg',
        //             lastModificationDate: '01/01/2137',
        //             lastModifiedBy: 'Bob',
        //         },
        //     ];
        // } else if (currentWorkspace.id === '2137') {
        //     this.files = [
        //         {
        //             id: '10',
        //             groupId: '2137',
        //             description: 'papaj kox',
        //             fileName: 'jp2gmd',
        //             file: 'base64',
        //             contentType: 'jpg',
        //             lastModificationDate: '01/01/2137',
        //             lastModifiedBy: 'Bob',
        //         },
        //         {
        //             id: '12',
        //             groupId: '2137',
        //             description: 'drugi opis',
        //             fileName: 'kremowka',
        //             file: 'base64',
        //             contentType: 'png',
        //             lastModificationDate: '01/01/2137',
        //             lastModifiedBy: 'Bob',
        //         },
        //         {
        //             id: '3',
        //             groupId: '2137',
        //             description: 'lol',
        //             fileName: 'cos2',
        //             file: 'base64',
        //             contentType: 'jpg',
        //             lastModificationDate: '01/01/2137',
        //             lastModifiedBy: 'Bob',
        //         },
        //         {
        //             id: '15',
        //             groupId: '2137',
        //             description: 'nauraElo',
        //             fileName: 'siema',
        //             file: 'base64',
        //             contentType: 'txt',
        //             lastModificationDate: '01/01/2137',
        //             lastModifiedBy: 'Bob',
        //         },
        //         {
        //             id: '30',
        //             groupId: '2137',
        //             description: 'baranowski',
        //             fileName: 'lululu',
        //             file: 'base64',
        //             contentType: 'jpg',
        //             lastModificationDate: '01/01/2137',
        //             lastModifiedBy: 'Bob',
        //         },
        //         {
        //             id: '44',
        //             groupId: '2137',
        //             description: 'dziwne bardzo',
        //             fileName: 'Konrad',
        //             file: 'base64',
        //             contentType: 'jpg',
        //             lastModificationDate: '01/01/2137',
        //             lastModifiedBy: 'Bob',
        //         },
        //     ];
        // } else if (currentWorkspace.id === '69') {
        //     this.files = [
        //         {
        //             id: '77',
        //             groupId: '69',
        //             description: 'dawaj obwod',
        //             fileName: 'pec',
        //             file: 'base64',
        //             contentType: 'jpg',
        //             lastModificationDate: '01/01/2137',
        //             lastModifiedBy: 'Bob',
        //         },
        //         {
        //             id: '80',
        //             groupId: '69',
        //             description: 'dawaj rybe',
        //             fileName: 'obiad',
        //             file: 'base64',
        //             contentType: 'png',
        //             lastModificationDate: '01/01/2137',
        //             lastModifiedBy: 'Bob',
        //         },
        //         {
        //             id: '83',
        //             groupId: '69',
        //             description: 'co do kurwy',
        //             fileName: 'lingwistyka',
        //             file: 'base64',
        //             contentType: 'jpg',
        //             lastModificationDate: '01/01/2137',
        //             lastModifiedBy: 'Bob',
        //         },
        //     ];
        // } else {
        //     console.log('no workspace selected!');
        // }

        this.isDataLoaded = false;

        this.mWorkspaceService.getWorkspacesOverview(currentWorkspace.id).subscribe(
            (result: FileModelDto[]) => {
                //console.log();
                //console.log(JSON.stringify(result));
                this.files = result;
                this.dataSource.data = this.files;
                this.isDataLoaded = true;
            },
            (error) => {
                console.log(error);
            }
        );
        //console.log(this.files);

        this.tableDisplay = {
            display: this.chosenWorkspace.id != null ? 'block' : 'none',
        };

        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
    }

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

    downloadFile(workspaceId: string, fileId: string, filename: string) {
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
            (error) => console.log('OHH shit: ' + JSON.stringify(error))
        );
    }
}
