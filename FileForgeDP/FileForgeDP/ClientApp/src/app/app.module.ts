import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { LoginFormComponent } from './home/login-form/login-form.component';
import { RegisterFormComponent } from './home/register-form/register-form.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { WorkspaceComponent } from './workspaces-overview/workspace-component/workspace.component';
import { WorkspacesOverviewComponent } from './workspaces-overview/workspaces-overview.component';

@NgModule({
    declarations: [AppComponent, NavMenuComponent, HomeComponent, LoginFormComponent, RegisterFormComponent, WorkspacesOverviewComponent, WorkspaceComponent, AdminPanelComponent],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        FormsModule,
        AppRoutingModule,
    ],
    providers: [],
    bootstrap: [AppComponent],
})
export class AppModule {}
