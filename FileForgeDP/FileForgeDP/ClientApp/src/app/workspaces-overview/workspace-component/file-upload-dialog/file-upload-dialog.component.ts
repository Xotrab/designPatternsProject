import { formatDate } from '@angular/common';
import { Component, EventEmitter, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { WorkspaceService } from 'src/app/services/workspace.service';
import { FileModelDto } from '../../../models/file/file-dto';


export interface DialogData {
    workspaceId: String;
}


@Component({
    selector: 'app-file-upload-dialog',
    templateUrl: './file-upload-dialog.component.html',
    styleUrls: ['./file-upload-dialog.component.scss'],
})
export class FileUploadDialogComponent implements OnInit {
    fileList: Array<FileModelDto> = new Array<FileModelDto>();

    isEmpty: boolean = true;
    isHovering: boolean;

    base64File: String;

    showDescription: boolean = false; //flag to determine whether app should show field for description
    currentlyDescribed: number; //Index of file to which user is  writing a description
    public description : String = "";

    isShaking: boolean = false;
    isOpen: boolean = true;
    isDone: { value: boolean } = { value: false }; // a little workaround to pass the flag by reference.
    fileUploadProgresses: Array<number> = new Array<number>();


    onUpload = new EventEmitter();

    constructor(
        private mSnackBar: MatSnackBar,
        private mWorkspaceService: WorkspaceService,
        public dialogRef: MatDialogRef<FileUploadDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: DialogData
    ) {}

    ngOnInit(): void {}

    toggleHover(event: boolean) {
        this.isHovering = event;
    }

    cutName(name: string): string {
        const boundary = 10;
        var parts = name.split('.');
        var extension;
        var filename;
        if (parts.length > 1) {
            extension = parts.pop();
            filename = parts.join('.');
        } else {
            filename = parts[0];
        }
        if (filename.length > boundary) {
            return parts[0].slice(0, boundary) + '...';
        } else return name;
    }

    onDescriptionFieldOpen(index : number)
    {
        this.showDescription = true;
        this.currentlyDescribed = index;
        this.description = this.fileList[index].description;
    }
    descriptionChange(index: number) {}

    handleDrop(fileList: FileList) {
        if (fileList.length + this.fileList.length > 3) {
            this.onAmountExceed();
        } else {
            Array.from(fileList).forEach((file) => {
                this.toBase64(file);
                this.fileList.push({
                    id: null,
                    groupId: this.data.workspaceId,
                    description: '(no description)',
                    fileName: file.name,
                    file: file,
                    contentType: file.type,
                    lastModificationDate: String(formatDate(new Date(), 'dd/MM/yyyy', 'en')),
                    lastModifiedBy: 'Bob',
                });
            });
            this.isEmpty = false;
        }
    }

    toBase64(file: File) {
        let me = this;
        let reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = function () {
            me.base64File = String(reader.result);
        };
        reader.onerror = function (error) {
            console.log('Error: ', error);
        };
    }

    removeFromUploadBuffer(index) {
        this.fileList.splice(index, 1);
        if (this.fileList.length == 0) this.isEmpty = true;
    }
    onCancel() {
        this.dialogRef.close();
    }
    onAmountExceed() {
        this.mSnackBar.open('Hey, slow down! Too many files!', 'Okay, sorry :<', {
            duration: 3000,
        });
        this.isShaking = true;
        setTimeout(() => {
            this.isShaking = false;
        }, 600);
    }
    onDescriptionSave()
    {
        var index = this.currentlyDescribed;
        this.showDescription=false;
        this.fileList[index].description = this.description;
        this.description = null;
        this.currentlyDescribed = null;
    }
    onDescriptionDiscard()
    {
        this.showDescription = false;
        this.description = null;
        this.currentlyDescribed = null;
    }

    uploadFiles() {
        this.isOpen = false;
        this.onUpload.emit({
            files: this.fileList,
            progressList: this.fileUploadProgresses,
            doneFlag: this.isDone,
        });
        while (!this.isDone) {
            console.log('Pobranko');
        }
    }
}
