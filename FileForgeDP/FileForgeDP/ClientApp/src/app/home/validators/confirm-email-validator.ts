import { FormGroup, ValidationErrors, ValidatorFn } from '@angular/forms';

/** password and confirm password must match each other **/
export const emailsMatchValidator: ValidatorFn = (control: FormGroup): ValidationErrors | null => {
    const email = control.get('email');
    const confirmEmail = control.get('confirmEmail');
    debugger;
    return email && confirmEmail && email.value === confirmEmail.value
        ? { emailsMatch: true }
        : { emailsMatch: false };
};
