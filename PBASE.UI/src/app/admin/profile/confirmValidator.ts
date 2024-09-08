import { FormGroup } from '@angular/forms';

export class RegistrationValidator {
    static validate(changePasswordForm: FormGroup) {
        let newPassword = changePasswordForm.controls.newPassword.value;
        let confPassword = changePasswordForm.controls.confPassword.value;

        if (confPassword.length <= 0) {
            return null;
        }

        if (confPassword !== newPassword) {
            return {
                doesMatchPassword: true
            };
        }

        return null;

    }
}