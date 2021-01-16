import { Component, OnInit, OnDestroy } from '@angular/core';
import { AdminService } from '../../services/admin.service';

@Component({
  selector: 'app-users-management',
  templateUrl: './users-management.component.html',
  styleUrls: ['./users-management.component.scss']
})
export class UsersManagementComponent implements OnInit, OnDestroy {

  constructor(private adminService: AdminService) { }

  ngOnInit(): void {
    setTimeout(() => this.synchronizeGroups(true), 60000);
  }

  ngOnDestroy(): void {
    this.synchronizeGroups(false);
  }

  synchronizeGroups(shouldCallInit: boolean) {
    this.adminService.synchronizeGroups().subscribe(response => {
      if (shouldCallInit) {
        this.ngOnInit();
      }
    });
  }

}
