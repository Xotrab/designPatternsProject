import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root',
})
export class WorkspaceService {
    constructor(private http: HttpClient) {}

    getWorkspacesOverview(id: string) {
        return this.http.get(environment.apiUrl + 'workspaces/' + id + '/files');
    }
}
