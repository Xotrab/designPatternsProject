import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkspacesOverviewComponent } from './workspaces-overview.component';

describe('WorkspacesOverviewComponent', () => {
  let component: WorkspacesOverviewComponent;
  let fixture: ComponentFixture<WorkspacesOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WorkspacesOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkspacesOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
