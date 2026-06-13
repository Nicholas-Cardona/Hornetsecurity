import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { finalize } from 'rxjs/operators';
import { AccountService, SignUpRequest } from '../../../../services/core/account';
import { InputComponent } from '../../../utils/input/text-input/text-input';
import { FormButton } from '../../../utils/input/form-button/form-button';
import { CardSkeleton } from "../../../utils/card/card-skeleton/card-skeleton";
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-sign-up-form',
  imports: [ReactiveFormsModule, InputComponent, FormButton, CardSkeleton],
  templateUrl: './sign-up-form.html',
  styleUrl: './sign-up-form.css',
})
export class SignUpForm {
  private account = inject(AccountService);
  isLoading = false;

  form = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
    confirmPassword: new FormControl('', [Validators.required]),
    firstName: new FormControl('', [Validators.required]),
    lastName: new FormControl('', [Validators.required])
  });

  private snackBar = inject(MatSnackBar)

  submit() {
    if (this.form.invalid) return;

    const value = this.form.value

    if (value.confirmPassword !== value.password) {
      this.snackBar.open("Password and Confirm Password do NOT match", "CLOSE",
        {
          panelClass: ['toast-error']
        }
      )
      return
    }

    this.isLoading = true

    const signUpRequest: SignUpRequest = {
      email: value.email!,
      firstName: value.firstName!,
      lastName: value.lastName!,
      password: value.password!,
      confirmPassword: value.confirmPassword!
    }

    this.account.signUp(signUpRequest).pipe(
      finalize(() => {
        this.isLoading = false;
      })
    )
      .subscribe({
        next: (res) => {
          console.log('Signed up:', res);
        },
        error: (err) => {
          console.error('Sign-up failed:', err);
        },
      });
  }
}
