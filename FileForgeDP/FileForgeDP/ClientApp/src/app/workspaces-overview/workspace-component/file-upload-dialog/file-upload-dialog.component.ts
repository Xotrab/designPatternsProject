import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FileModelDto } from '../../../models/file/file-dto';

import { formatDate } from '@angular/common';
import { MatSnackBar } from '@angular/material/snack-bar';
import { HttpErrorResponse, HttpEventType } from '@angular/common/http';
import { WorkspaceService } from 'src/app/services/workspace.service';
import { catchError, map } from 'rxjs/operators';

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

    fileUploadProgresses: Array<number> = new Array<number>();

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
    onExceed() {
        this.mSnackBar.open('Hey, slow down! Too many files!', 'Okay, sorry :<', {
            duration: 3000,
        });
        this.isShaking = true;
        setTimeout(() => {
            this.isShaking = false;
        }, 600);
    }

    uploadFiles() {
        var count = 0;
        this.fileList.forEach((file) => {
            this.uploadFile(count++, file);
            this.fileUploadProgresses.push(0);
        });
    }

    uploadFile(fileNumber: number, file: FileModelDto) {
        const formatData = new FormData();
        formatData.append('file', file.file);
        formatData.append('fileName', file.file.name);
        formatData.append('lastModificationDate', <string>file.lastModificationDate);
        formatData.append('description', <string>file.description);
        formatData.append('lastModifiedBy', <string>file.lastModifiedBy);

        this.mWorkspaceService
            .uploadWorkspaceFile(file.groupId, formatData)
            .pipe(
                map((event) => {
                    switch (event.type) {
                        case HttpEventType.UploadProgress:
                            this.fileUploadProgresses[fileNumber] = Math.round(
                                (event.loaded * 100) / event.total
                            );
                            console.log(this.fileUploadProgresses);
                            break;
                        case HttpEventType.Response:
                            return event;
                    }
                })
            )
            .subscribe((event: any) => {
                if (typeof event === 'object') {
                    console.log(event.body);
                }
            });
    }
}
