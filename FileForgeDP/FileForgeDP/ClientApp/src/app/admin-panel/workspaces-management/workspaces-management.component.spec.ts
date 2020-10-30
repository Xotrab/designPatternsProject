import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkspacesManagementComponent } from './workspaces-management.component';

describe('WorkspacesManagementComponent', () => {
  let component: WorkspacesManagementComponent;
  let fixture: ComponentFixture<WorkspacesManagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WorkspacesManagementComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkspacesManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
