import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { OAuthService } from 'angular-oauth2-oidc';

@Injectable({
    providedIn: 'root',
})
export class WorkspaceService {
    constructor(private http: HttpClient, private oauthService: OAuthService) {}

    getWorkspacesOverview(id: String) {
        var headers = new HttpHeaders({
          "Authorization": "Bearer " + this.oauthService.getAccessToken()
        });
        return this.http.get(environment.apiUrl + 'workspaces/' + id + '/files', { headers: headers });
    }

    public uploadWorkspaceFile(worksapceId: String, form: FormData) {
        var headers = new HttpHeaders({
          "Authorization": "Bearer " + this.oauthService.getAccessToken()
        });
        return this.http.post<any>(environment.apiUrl + 'workspaces/' + worksapceId, form, {
            headers: headers,
            reportProgress: true,
            observe: 'events',
        });
    }

    public downloadWorkspaceFile(workspaceId: string, fileId: string) {
        var headers = new HttpHeaders({
          "Authorization": "Bearer " + this.oauthService.getAccessToken()
        });
        return this.http.get(
            environment.apiUrl + 'workspaces/' + workspaceId + '/files/' + fileId,
            { headers: headers, responseType: 'blob' as 'json' }
        );
    }
}
