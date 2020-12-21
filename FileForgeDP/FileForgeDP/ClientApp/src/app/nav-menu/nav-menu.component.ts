import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
    selector: 'app-nav-menu',
    templateUrl: './nav-menu.component.html',
    styleUrls: ['./nav-menu.component.scss'],
})
export class NavMenuComponent {
    isExpanded = false;

    constructor(private oauthService: OAuthService, private router: Router) {}

    collapse() {
        this.isExpanded = false;
    }

    toggle() {
        this.isExpanded = !this.isExpanded;
    }

    logout() {
        this.oauthService.logOut();
        this.router.navigate(['/']);
    }

    get givenName() {
        let claims = <any>this.oauthService.getIdentityClaims();
        if (!claims) return null;
        return claims.preferred_username;
    }
}
