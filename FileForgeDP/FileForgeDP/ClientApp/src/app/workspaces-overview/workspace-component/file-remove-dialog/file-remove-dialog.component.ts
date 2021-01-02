import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

export interface RemoveDialogData{
  filename : string;
}


@Component({
  selector: 'app-file-remove-dialog',
  templateUrl: './file-remove-dialog.component.html',
  styleUrls: ['./file-remove-dialog.component.scss']
})
export class FileRemoveDialogComponent implements OnInit {

  constructor(
    public dialogRef : MatDialogRef<FileRemoveDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data : RemoveDialogData
  ) { }

  ngOnInit(): void {
  }

  onCancel(){
    this.dialogRef.close('cancel');
  }

  onRemove(){
    this.dialogRef.close('accept');
  }

}
