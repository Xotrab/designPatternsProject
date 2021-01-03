import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FileUpdateDialogComponent } from './file-update-dialog.component';

describe('FileUpdateDialogComponent', () => {
  let component: FileUpdateDialogComponent;
  let fixture: ComponentFixture<FileUpdateDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FileUpdateDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FileUpdateDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
