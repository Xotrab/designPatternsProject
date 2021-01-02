import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FileRemoveDialogComponent } from './file-remove-dialog.component';

describe('FileRemoveDialogComponent', () => {
  let component: FileRemoveDialogComponent;
  let fixture: ComponentFixture<FileRemoveDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FileRemoveDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FileRemoveDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
