import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
    selector: 'app-register-form',
    templateUrl: './register-form.component.html',
    styleUrls: ['./register-form.component.scss'],
})
export class RegisterFormComponent implements OnInit {
    constructor(private builder: FormBuilder) {}
    public confirmEmailValid: boolean;

    public formLogin: FormGroup;
    ngOnInit(): void {
        this.formLogin = this.builder.group({
            login: ['', [Validators.required, Validators.minLength(4)]],
            email: ['', [Validators.required, Validators.email]],
            confirmEmail: ['', [Validators.required]],
            password: ['', [Validators.required, Validators.minLength(5)]],
            confirmPassword: ['', [Validators.required]],
        });

        this.confirmEmailValid = this.confirmEmail.valid;
    }

    get f() {
        return this.formLogin.controls;
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
        console.log(this.formLogin);
        return this.formLogin.get('confirmEmail');
    }

    onSubmit() {}

    equalEmails() {
        const matched = this.email.value === this.confirmEmail.value;
        if (matched) {
            this.formLogin.controls.confirmEmail.setErrors(null);
        } else {
            this.formLogin.controls.confirmEmail.setErrors({
                emaisMismatch: true,
            });
        }

        return matched;
    }
    equalPasswords() {
        const matched = this.password.value === this.confirmPassword.value;
        if (matched) {
            this.formLogin.controls.confirmPassword.setErrors(null);
        } else {
            this.formLogin.controls.confirmPassword.setErrors({
                passwordsMismatch: true,
            });
        }

        return matched;
    }
}
