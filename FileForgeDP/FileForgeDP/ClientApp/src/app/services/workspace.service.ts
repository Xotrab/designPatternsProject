import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root',
})
export class WorkspaceService {
    constructor(private http: HttpClient) {}

    getWorkspacesOverview(id: String) {
        return this.http.get(environment.apiUrl + 'workspaces/' + id + '/files');
    }

    public uploadWorkspaceFile(worksapceId: String, form: FormData) {
        return this.http.post<any>(environment.apiUrl + 'workspaces/' + worksapceId, form, {
            reportProgress: true,
            observe: 'events',
        });
    }

    public downloadWorkspaceFile(workspaceId: string, fileId: string) {
        return this.http.get(
            environment.apiUrl + 'workspaces/' + workspaceId + '/files/' + fileId,
            { responseType: 'blob' as 'json' }
        );
    }
}
