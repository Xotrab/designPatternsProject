import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

export interface UpdateDialogData{
  filename : string;
  newFilename : string;
  newDescription : string;
}

@Component({
  selector: 'app-file-update-dialog',
  templateUrl: './file-update-dialog.component.html',
  styleUrls: ['./file-update-dialog.component.scss']
})
export class FileUpdateDialogComponent implements OnInit {

  constructor(
    private mSnackBar: MatSnackBar,
    public dialogRef : MatDialogRef<FileUpdateDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data : UpdateDialogData
  ) { }

  ngOnInit(): void {
  }

  onCancel(){
    this.dialogRef.close({decision: "cancel", data:null});
  }

  onSubmit(){
    if(this.data.newFilename != "" || this.data.newDescription !=""){
      this.dialogRef.close({decision: "update", data: this.data});
    }
    else{
      this.mSnackBar.open('At least one input is required!', 'Got it', {
      duration: 3000,
      });
    }

  }

}
