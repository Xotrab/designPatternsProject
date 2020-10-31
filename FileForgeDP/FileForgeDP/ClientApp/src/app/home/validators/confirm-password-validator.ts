import { FormGroup, ValidationErrors, ValidatorFn } from '@angular/forms';

/** password and confirm password must match each other **/
export const passwordsMatchValidator: ValidatorFn = (
    control: FormGroup
): ValidationErrors | null => {
    const password = control.get('password');
    const confirmPassword = control.get('confirmPassword');
    return password && confirmPassword && password.value === confirmPassword.value
        ? { passwordsMatch: true }
        : { passwordsMatch: false };
};
