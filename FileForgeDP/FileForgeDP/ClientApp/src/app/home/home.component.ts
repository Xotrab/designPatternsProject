import { OAuthService } from 'angular-oauth2-oidc';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
    public links: Array<string> = new Array<string>('Log in !', 'Create an account');

    public link: string = this.links[1];

    constructor(private oauthService: OAuthService, private router: Router) {}

    ngOnInit(): void {
        if (this.oauthService.hasValidAccessToken()) {
            this.router.navigate(['/workspaces']);
        }
    }

    changeLinkAndComponent() {
        this.link = this.link == this.links[0] ? this.links[1] : this.links[0];
    }

}
