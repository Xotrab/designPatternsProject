import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
    selector: 'app-login-form',
    templateUrl: './login-form.component.html',
    styleUrls: ['./login-form.component.scss'],
})
export class LoginFormComponent implements OnInit {
    hide: boolean = true;

    login: string = '';
    password: string = '';

    constructor(private oauthService: OAuthService) {}

    ngOnInit(): void {}

    onSubmit() {
        this.oauthService.initImplicitFlow();
    }
}
