import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { HomeComponent } from './home/home.component';
import { WorkspacesOverviewComponent } from './workspaces-overview/workspaces-overview.component';

const routes: Routes = [
    { path: 'admin', component: AdminPanelComponent},
    { path: 'workspaces', component: WorkspacesOverviewComponent},   
    { path: '**', component: HomeComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
    declarations: [],
})
export class AppRoutingModule {}
