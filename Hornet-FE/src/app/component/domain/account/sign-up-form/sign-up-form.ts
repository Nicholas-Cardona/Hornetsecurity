import { Component, inject, signal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { finalize } from 'rxjs/operators';
import { AccountService, SignUpRequest } from '../../../../services/core/account';
import { FormButton } from '../../../utils/input/form-button/form-button';
import { CardSkeleton } from "../../../utils/card/card-skeleton/card-skeleton";
import { MatSnackBar } from '@angular/material/snack-bar';
import { CustomInput } from '../../../utils/input/custom-input/custom-input';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-up-form',
  imports: [ReactiveFormsModule, CustomInput, FormButton, CardSkeleton],
  templateUrl: './sign-up-form.html',
  styleUrl: './sign-up-form.css',
})
export class SignUpForm {
  private account = inject(AccountService);
  private router = inject(Router)
  private snackBar = inject(MatSnackBar)
  isLoading = signal(false);

  form = new FormGroup({
    email: new FormControl('', [Validators.email]),
    password: new FormControl('', [Validators.required, Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9]).{8,}$/)]),
    confirmPassword: new FormControl('', [Validators.required]),
    firstName: new FormControl('', [Validators.required]),
    lastName: new FormControl('', [Validators.required])
  });

  get password() {
    return this.form.get('password');
  }

  get confirmPassword() {
    return this.form.get('confirmPassword');
  }


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

    this.isLoading.set(true)

    const signUpRequest: SignUpRequest = {
      email: value.email!,
      firstName: value.firstName!,
      lastName: value.lastName!,
      password: value.password!,
      passwordConfirm: value.confirmPassword!
    }

    this.account.signUp(signUpRequest).pipe(
      finalize(() => {
        this.isLoading.set(false)
      })
    )
      .subscribe({
        next: (res) => {
          this.router.navigate(["dash"])
        },
        error: (err) => {
          this.snackBar.open("Error when signing up", "CLOSE",
            {
              panelClass: ['toast-error'],
            }
          )
          console.error('Sign-up failed:', err);
        },
      });
  }
}
