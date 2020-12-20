import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
    selector: 'app-login-form',
    templateUrl: './login-form.component.html',
    styleUrls: ['./login-form.component.scss'],
})
export class LoginFormComponent implements OnInit {
    hide: boolean = true;

    constructor(private oauthService: OAuthService) {}

    ngOnInit(): void {}

    login() {
        this.oauthService.initImplicitFlow();
    }
}
