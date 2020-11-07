import { Component, Input, OnChanges, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { WorkspaceModelDto } from '../../models/workspace/workspace-dto';
import { FileModelDto } from '../../models/file/file-dto';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import {animate, state, style, transition, trigger} from '@angular/animations';
import { environment } from 'src/environments/environment';

@Component({
    selector: 'app-workspace',
    templateUrl: './workspace.component.html',
    styleUrls: ['./workspace.component.scss'],
    animations: [
        trigger('detailExpand', [
          state('collapsed', style({height: '0px', minHeight: '0'})),
          state('expanded', style({height: '*'})),
          transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
        ]),
      ],
})
export class WorkspaceComponent implements OnInit, OnChanges {
    constructor() {}

    @Input() public chosenWorkspace: WorkspaceModelDto;

    columnsToDisplay = ['fileName', 'contentType', 'lastModificationDate', 'lastModificationBy'];

    files: FileModelDto[];

    dataSource : MatTableDataSource<FileModelDto>;

    @ViewChild(MatPaginator) paginator: MatPaginator;

    @ViewChild(MatSort) sort: MatSort;

    pageSizeOptions = environment.workspacePageSize;

    tableDisplay = {}

    expandedElement: FileModelDto | null;

    ngOnInit(): void {}

    ngOnChanges(changes: SimpleChanges) {
        //later a place for ../api/workspaces/ID/files GET method
        console.log("jd");
        const workspaceChanges = changes['chosenWorkspace'];
        const currentWorkspace : WorkspaceModelDto = workspaceChanges.currentValue;
        if(currentWorkspace.id ==="1"){
            this.files = [
                {id: '4', groupId:'1', description:"420 blaze it", fileName:"jaranko",file:"base64",contentType:"jpg",lastModificationDate:"01/01/2137",lastModifiedBy:"Bob"},
                {id: '2', groupId:'1', description:"opis tak o", fileName:"jaranko2",file:"base64",contentType:"png",lastModificationDate:"01/01/2137",lastModifiedBy:"Bob"},
                {id: '0', groupId:'1', description:"xd", fileName:"cos",file:"base64",contentType:"jpg",lastModificationDate:"01/01/2137",lastModifiedBy:"Bob"},
                {id: '6', groupId:'1', description:"naura", fileName:"masno",file:"base64",contentType:"txt",lastModificationDate:"01/01/2137",lastModifiedBy:"Bob"},
                {id: '9', groupId:'1', description:"skun mocny", fileName:"weedMaster",file:"base64",contentType:"jpg",lastModificationDate:"01/01/2137",lastModifiedBy:"Bob"},
                {id: '4', groupId:'1', description:"x420 blaze it", fileName:"jaranko",file:"base64",contentType:"jpg",lastModificationDate:"01/01/2137",lastModifiedBy:"Bob"},
                {id: '2', groupId:'1', description:"xopis tak o", fileName:"jaranko2",file:"base64",contentType:"png",lastModificationDate:"01/01/2137",lastModifiedBy:"Bob"},
                {id: '0', groupId:'1', description:"xxd", fileName:"cos",file:"base64",contentType:"jpg",lastModificationDate:"01/01/2137",lastModifiedBy:"Bob"},
                {id: '6', groupId:'1', description:"xnaura", fileName:"masno",file:"base64",contentType:"txt",lastModificationDate:"01/01/2137",lastModifiedBy:"Bob"},
                {id: '9', groupId:'1', description:"xskun mocny", fileName:"weedMaster",file:"base64",contentType:"jpg",lastModificationDate:"01/01/2137",lastModifiedBy:"Bob"},
                {id: '9', groupId:'1', description:"skun mocny", fileName:"weedMaster",file:"base64",contentType:"jpg",lastModificationDate:"01/01/2137",lastModifiedBy:"Bob"},
                {id: '4', groupId:'1', description:"x420 blaze it", fileName:"jaranko",file:"base64",contentType:"jpg",lastModificationDate:"01/01/2137",lastModifiedBy:"Bob"},
                {id: '2', groupId:'1', description:"xopis tak o", fileName:"jaranko2",file:"base64",contentType:"png",lastModificationDate:"01/01/2137",lastModifiedBy:"Bob"},
                {id: '0', groupId:'1', description:"xxd", fileName:"cos",file:"base64",contentType:"jpg",lastModificationDate:"01/01/2137",lastModifiedBy:"Bob"},
                {id: '6', groupId:'1', description:"xnaura", fileName:"masno",file:"base64",contentType:"txt",lastModificationDate:"01/01/2137",lastModifiedBy:"Bob"},
                {id: '9', groupId:'1', description:"chujxskun mocny", fileName:"weedMaster",file:"base64",contentType:"jpg",lastModificationDate:"01/01/2137",lastModifiedBy:"Bob"}
            ];
        }
        else if(currentWorkspace.id ==="2137"){
            this.files = [
                {id: '10', groupId:'2137', description:"papaj kox", fileName:"jp2gmd",file:"base64",contentType:"jpg",lastModificationDate:"01/01/2137",lastModifiedBy:"Bob"},
                {id: '12', groupId:'2137', description:"drugi opis", fileName:"kremowka",file:"base64",contentType:"png",lastModificationDate:"01/01/2137",lastModifiedBy:"Bob"},
                {id: '3', groupId:'2137', description:"lol", fileName:"cos2",file:"base64",contentType:"jpg",lastModificationDate:"01/01/2137",lastModifiedBy:"Bob"},
                {id: '15', groupId:'2137', description:"nauraElo", fileName:"siema",file:"base64",contentType:"txt",lastModificationDate:"01/01/2137",lastModifiedBy:"Bob"},
                {id: '30', groupId:'2137', description:"baranowski", fileName:"lululu",file:"base64",contentType:"jpg",lastModificationDate:"01/01/2137",lastModifiedBy:"Bob"},
                {id: '44', groupId:'2137', description:"dziwne bardzo", fileName:"Konrad",file:"base64",contentType:"jpg",lastModificationDate:"01/01/2137",lastModifiedBy:"Bob"}
            ];
        }
        else if(currentWorkspace.id ==="69"){
            this.files = [
                {id: '77', groupId:'69', description:"dawaj obwod", fileName:"pec",file:"base64",contentType:"jpg",lastModificationDate:"01/01/2137",lastModifiedBy:"Bob"},
                {id: '80', groupId:'69', description:"dawaj rybe", fileName:"obiad",file:"base64",contentType:"png",lastModificationDate:"01/01/2137",lastModifiedBy:"Bob"},
                {id: '83', groupId:'69', description:"co do kurwy", fileName:"lingwistyka",file:"base64",contentType:"jpg",lastModificationDate:"01/01/2137",lastModifiedBy:"Bob"},
            ];
        }
        else{
            console.log("no workspace selected!");
        }
        this.tableDisplay = {
            'display': this.chosenWorkspace.id != null  ? 'block' : 'none'
        }
    
        this.dataSource = new MatTableDataSource([]);
        this.dataSource.data = this.files;
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      }

      applyFilter(event: Event) {
        this.dataSource.filterPredicate = function(data, filter: string): boolean {
            return data.fileName.toLowerCase().includes(filter);
        };
        const filterValue = (event.target as HTMLInputElement).value;
        this.dataSource.filter = filterValue.trim().toLowerCase();
    
        if (this.dataSource.paginator) {
          this.dataSource.paginator.firstPage();
        }
      }

      
}
