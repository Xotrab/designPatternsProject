import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { equal } from 'assert';
import { UserRegisterDto } from '../../models/user/user-register-dto';


@Component({
    selector: 'app-register-form',
    templateUrl: './register-form.component.html',
    styleUrls: ['./register-form.component.scss'],
})
export class RegisterFormComponent implements OnInit {
    constructor(private builder: FormBuilder, private userService: UserService) {}
    public confirmEmailValid: boolean;

    @Output() registered = new EventEmitter<any>();

    public formLogin: FormGroup;
    ngOnInit(): void {
        this.formLogin = this.builder.group({
            firstName: ['', [Validators.required, Validators.minLength(1)]],
            lastName: ['', [Validators.required, Validators.minLength(1)]],
            login: ['', [Validators.required, Validators.minLength(1)]],
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
    get firstName() {
        return this.formLogin.get('firstName');
    }
    get lastName() {
        return this.formLogin.get('lastName');
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

    onSubmit() {
        if (!this.equalEmails() || !this.equalPasswords()) {
            return;
        }

        var userRegisterDto = {
            firstName: this.firstName.value,
            lastName: this.lastName.value,
            email: this.email.value,
            username: this.login.value,
            password: this.password.value,
        } as UserRegisterDto;
        this.userService.registerUser(userRegisterDto).subscribe((x) => console.log(x));
        this.registered.emit("registered");
    }

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
