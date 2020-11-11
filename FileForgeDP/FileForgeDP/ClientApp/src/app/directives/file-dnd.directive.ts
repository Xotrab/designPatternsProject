import { Directive, EventEmitter, HostListener, Output } from '@angular/core';

@Directive({
  selector: '[appFileDnd]'
})
export class FileDndDirective {

  @Output() filesDropped = new EventEmitter<FileList>();
  @Output() filesHovered = new EventEmitter<boolean>();


  constructor() {}

  @HostListener('drop',['$event']) onDrop(event){

    
    event.preventDefault();
    event.stopPropagation();

    let files = event.dataTransfer.files;
    
    if(files.length > 0){
      this.filesDropped.emit(files);
    }
    this.filesHovered.emit(false);
  }
  
  @HostListener('dragover', ['$event']) onDragOver($event){

    
    $event.preventDefault();
    $event.stopPropagation();

    this.filesHovered.emit(true);


  }

  @HostListener('dragleave', ['$event']) onDragLeave($event){
    
    $event.preventDefault();
    $event.stopPropagation();

    this.filesHovered.emit(false);
  }

}
