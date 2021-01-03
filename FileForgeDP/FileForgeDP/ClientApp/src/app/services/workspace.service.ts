import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { OAuthService } from 'angular-oauth2-oidc';
import { FileModelDto } from '../models/file/file-dto';

@Injectable({
    providedIn: 'root',
})
export class WorkspaceService {
    constructor(private http: HttpClient, private oauthService: OAuthService) {}

    public getWorkspaceFiles(id: String) {
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

    public removeWorkspaceFile(workspaceId : string, fileId: string){
        return this.http.delete(environment.apiUrl + 'workspaces/' + workspaceId + '/files/' + fileId);
    }

    public updateWorkspaceFile(workspaceId: String, fileId: String, update: FileModelDto){
        return this.http.put(environment.apiUrl + 'workspaces/' + workspaceId + '/files/' + fileId, update);
    }
}
