import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { UsersManagementComponent } from './admin-panel/users-management/users-management.component';
import { WorkspacesManagementComponent } from './admin-panel/workspaces-management/workspaces-management.component';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { LoginFormComponent } from './home/login-form/login-form.component';
import { RegisterFormComponent } from './home/register-form/register-form.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { WorkspaceComponent } from './workspaces-overview/workspace-component/workspace.component';
import { WorkspacesOverviewComponent } from './workspaces-overview/workspaces-overview.component';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatRippleModule } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        LoginFormComponent,
        RegisterFormComponent,
        WorkspacesOverviewComponent,
        WorkspaceComponent,
        AdminPanelComponent,
        UsersManagementComponent,
        WorkspacesManagementComponent,
    ],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        FormsModule,
        AppRoutingModule,
        MatInputModule,
        MatFormFieldModule,
        MatButtonModule,
        MatRippleModule,
        MatIconModule,
        BrowserAnimationsModule,
    ],
    providers: [],
    bootstrap: [AppComponent],
})
export class AppModule {}
