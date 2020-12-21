import { AfterViewChecked } from '@angular/core';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
    selector: 'app-login-form',
    templateUrl: './login-form.component.html',
    styleUrls: ['./login-form.component.scss'],
})
export class LoginFormComponent implements OnInit, AfterViewChecked {
    hide: boolean = true;

    constructor(private oauthService: OAuthService, private router: Router) {}

    // check if user succesfully logged in app and redirect to workspaces
    ngAfterViewChecked(): void {
        if (this.oauthService.hasValidAccessToken()) {
            this.router.navigate(['/workspaces']);
        }
    }

    ngOnInit(): void {}

    login() {
        this.oauthService.initImplicitFlow();
    }
}
