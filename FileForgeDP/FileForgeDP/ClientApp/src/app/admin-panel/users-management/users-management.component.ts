import { Component, OnInit, OnDestroy } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { environment } from 'src/environments/environment';
import { AdminService } from '../../services/admin.service';

@Component({
  selector: 'app-users-management',
  templateUrl: './users-management.component.html',
  styleUrls: ['./users-management.component.scss']
})
export class UsersManagementComponent implements OnInit, OnDestroy {

    connection: signalR.HubConnection;

    constructor(private adminService: AdminService) { }

    ngOnInit(): void {
        this.signalRConnect();
        setTimeout(() => this.synchronizeGroups(true), 60000);
    }

    ngOnDestroy(): void {
        this.connection.stop();
    }

    synchronizeGroups(shouldCallInit: boolean) {
        this.adminService.synchronizeGroups().subscribe(response => {
            if (shouldCallInit) {
                this.ngOnInit();
            }
        });
    }

    signalRConnect() {
		this.connection = new signalR.HubConnectionBuilder()
			.configureLogging(signalR.LogLevel.Information)
			.withUrl(environment.apiUrl + "admin/notify")
			.build();

		this.connection.keepAliveIntervalInMilliseconds = 24 * 60 * 1000;
		this.connection.serverTimeoutInMilliseconds = 24 * 60 * 1000;

		this.connection
			.start()
			.then(() => {
				this.connection.invoke("JoinGroup", "Admin");
			})
			.catch(function (err) {
				return console.error(err.toString());
			});

		this.connection.onclose(() => {
			this.connection
				.start()
				.then(() => {
					this.connection.invoke("JoinGroup", "Admin");
				})
				.catch(function (err) {
					return console.error(err.toString());
				});
		});
  }

}
