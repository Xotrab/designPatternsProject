import { AuthGuard } from './security/authGuard';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { HomeComponent } from './home/home.component';
import { WorkspacesOverviewComponent } from './workspaces-overview/workspaces-overview.component';

const routes: Routes = [
    {
        path: 'admin',
        component: AdminPanelComponent,
        canActivate: [AuthGuard],
        data: { expectedRole: 'admin' },
    },
    { path: 'workspaces', component: WorkspacesOverviewComponent, canActivate: [AuthGuard] },
    { path: '**', component: HomeComponent },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
    declarations: [],
})
export class AppRoutingModule {}
