import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

export interface UpdateDialogData {
    filename: string;
    newFilename: string;
    newDescription: string;
    description: string;
}

@Component({
    selector: 'app-file-update-dialog',
    templateUrl: './file-update-dialog.component.html',
    styleUrls: ['./file-update-dialog.component.scss'],
})
export class FileUpdateDialogComponent implements OnInit {
    constructor(
        private mSnackBar: MatSnackBar,
        public dialogRef: MatDialogRef<FileUpdateDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: UpdateDialogData
    ) {}

    filenameChecked: boolean = false;
    descriptionChecked: boolean = false;

    filenameRegex = /^[^\\/:\*\?"<>\|.]+$/;

    ngOnInit(): void {}

    toggleFilename(event) {
        this.filenameChecked = event.checked;
        if (!this.filenameChecked) {
            this.data.newFilename = '';
        } else {
            this.data.newFilename = this.data.filename.split('.')[0];
        }
    }

    toggleDescription(event) {
        this.descriptionChecked = event.checked;
        if (!this.descriptionChecked) {
            this.data.newDescription = '';
        } else {
            this.data.newDescription = this.data.description;
        }
    }

    onCancel() {
        this.dialogRef.close({ decision: 'cancel', data: null });
    }

    onSubmit() {
        if (this.filenameChecked || this.descriptionChecked) {
            var filenameValid = true;

            if (
                this.filenameChecked &&
                (this.data.newFilename == '' || !this.filenameRegex.test(this.data.newFilename))
            ) {
                filenameValid = false;
            }
            if (filenameValid) {
                if(this.data.newDescription=="") this.data.newDescription="(no description)";
                this.dialogRef.close({ decision: 'update', data: this.data });
            } else {
                this.mSnackBar.open(
                    'Error - filename is empty or it includes special characters!',
                    'Got it',
                    {
                        duration: 4000,
                    }
                );
            }
        } else {
            this.mSnackBar.open('At least one box has to be checked!', 'Got it', {
                duration: 4000,
            });
        }
    }
}
