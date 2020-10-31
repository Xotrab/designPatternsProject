import { Component, OnInit, ViewEncapsulation } from '@angular/core';

@Component({
    selector: 'app-login-form',
    templateUrl: './login-form.component.html',
    styleUrls: ['./login-form.component.scss'],
})
export class LoginFormComponent implements OnInit {
    hide: boolean = true;

    login: string = '';
    password: string = '';

    constructor() {}

    ngOnInit(): void {}

    onSubmit() {}
}
