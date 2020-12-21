import { OAuthService } from 'angular-oauth2-oidc';
import { Injectable } from '@angular/core';
import {
    ActivatedRouteSnapshot,
    CanActivate,
    Router,
    RouterStateSnapshot,
    UrlTree,
} from '@angular/router';
import { Observable } from 'rxjs';

@Injectable()
export class AuthGuard implements CanActivate {
    constructor(private oauthService: OAuthService, private router: Router) {}

    canActivate(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
        if (this.ifAuthTokenValid() && this.ifAdminRequiredCheckClaims(route.data.expectedRole)) {
            return true;
        }
        this.router.navigate(['/']);
        return false;
    }

    ifAuthTokenValid(): boolean {
        return this.oauthService.hasValidAccessToken();
    }

    ifAdminRequiredCheckClaims(expectedrole: any): boolean {
        if (expectedrole == null) {
            return true;
        }
        var claims = <any>this.oauthService.getIdentityClaims();
        var result = claims.preferred_username == 'admin' ? true : false;
        return result;
    }
}
