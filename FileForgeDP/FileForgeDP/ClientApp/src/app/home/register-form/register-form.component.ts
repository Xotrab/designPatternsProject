import { emailsMatchValidator } from './../validators/confirm-email-validator';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { passwordsMatchValidator } from '../validators/confirm-password-validator';

@Component({
    selector: 'app-register-form',
    templateUrl: './register-form.component.html',
    styleUrls: ['./register-form.component.scss'],
})
export class RegisterFormComponent implements OnInit {
    constructor(private builder: FormBuilder) {}

    public formLogin: FormGroup;
    ngOnInit(): void {
        this.formLogin = this.builder.group(
            {
                login: ['', [Validators.required, Validators.minLength(4)]],
                email: ['', [Validators.required, Validators.email]],
                confirmEmail: ['', [Validators.required, Validators.email]],
                password: ['', [Validators.required, Validators.minLength(5)]],
                confirmPassword: ['', [Validators.required, Validators.minLength(5)]],
            },
            { validators: [passwordsMatchValidator, emailsMatchValidator] }
        );
    }

    get email() {
        return this.formLogin.get('email');
    }
    get password() {
        return this.formLogin.get('password');
    }
    get confirmPassword() {
        return this.formLogin.get('confirmPassword');
    }
    get login() {
        return this.formLogin.get('login');
    }
    get confirmEmail() {
        return this.formLogin.get('confirmEmail');
    }

    onSubmit() {}
}
