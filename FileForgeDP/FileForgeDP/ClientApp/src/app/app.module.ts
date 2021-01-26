import { AuthInterceptor } from './security/authInterceptor';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { UsersManagementComponent } from './admin-panel/users-management/users-management.component';
import { WorkspacesManagementComponent } from './admin-panel/workspaces-management/workspaces-management.component';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
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
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatDividerModule } from '@angular/material/divider';
import { MatListModule } from '@angular/material/list';
import { MatTableModule } from '@angular/material/table';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatProgressBarModule } from '@angular/material/progress-bar'; 
import {MatCheckboxModule} from '@angular/material/checkbox';
import { MatDialogModule } from '@angular/material/dialog';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatToolbarModule } from '@angular/material/toolbar';
import { ReactiveFormsModule } from '@angular/forms';
import { WorkspaceSidebarComponent } from './workspaces-overview/workspace-sidebar/workspace-sidebar.component';
import { FileUploadDialogComponent } from './workspaces-overview/workspace-component/file-upload-dialog/file-upload-dialog.component';
import { FileDndDirective } from './directives/file-dnd.directive';
import { UserService } from './services/user.service';
import { OAuthModule } from 'angular-oauth2-oidc';
import { AuthGuard } from './security/authGuard';
import { FileRemoveDialogComponent } from './workspaces-overview/workspace-component/file-remove-dialog/file-remove-dialog.component';
import { FileUpdateDialogComponent } from './workspaces-overview/workspace-component/file-update-dialog/file-update-dialog.component';
import { AdminService } from './services/admin.service';
import { HomeComponent } from './home/home.component';
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
        WorkspaceSidebarComponent,
        WorkspaceComponent,
        FileUploadDialogComponent,
        FileDndDirective,
        FileRemoveDialogComponent,
        FileUpdateDialogComponent,
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
        MatToolbarModule,
        MatSidenavModule,
        MatDividerModule,
        MatListModule,
        MatTableModule,
        MatPaginatorModule,
        MatSortModule,
        MatDialogModule,
        ReactiveFormsModule,
        MatSnackBarModule,
        MatProgressBarModule,
        MatCheckboxModule,
        OAuthModule.forRoot(),
    ],
    providers: [
        UserService,
        AdminService,
        AuthGuard,
        {
            provide: HTTP_INTERCEPTORS,
            useClass: AuthInterceptor,
            multi: true,
        },
    ],
    bootstrap: [AppComponent],
})
export class AppModule {}
