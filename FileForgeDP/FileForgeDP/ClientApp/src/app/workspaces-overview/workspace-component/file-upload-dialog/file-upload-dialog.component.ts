import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FileModelDto } from '../../../models/file/file-dto';
import { WorkspaceService } from '../../../services/workspace.service';
import { formatDate } from '@angular/common';
import { MatSnackBar } from '@angular/material/snack-bar';

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
    isShaking: boolean = false;

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

    handleDrop(fileList: FileList) {
        if (fileList.length + this.fileList.length > 3) {
            this.onExceed();
        } else {
            Array.from(fileList).forEach((file) => {
                this.toBase64(file);
                this.fileList.push({
                    id: null,
                    groupId: this.data.workspaceId,
                    description: '(no description)',
                    fileName: file.name,
                    file: this.base64File,
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
    onExceed() {
        this.mSnackBar.open('Hey, slow down! Too many files!', 'Okay, sorry :<', {
            duration: 5000,
        });
        this.isShaking = true;
        setTimeout(() => {
            this.isShaking = false;
        }, 600);
    }
    uploadFiles() {}
}
