import { Component, inject, signal } from '@angular/core';
import { CardSkeleton } from '@components/utils/card/card-skeleton/card-skeleton';
import { CustomInput } from '@components/utils/input/custom-input/custom-input';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FormButton } from '@components/utils/input/form-button/form-button';
import { AccountService, SignInRequest } from '@services/core/account';
import { finalize, tap } from 'rxjs';
import { MatCheckbox } from '@angular/material/checkbox';
import { Router } from '@angular/router';
import { MatSnackBar, MatSnackBarAction } from '@angular/material/snack-bar';
import { HttpErrorResponse } from '@angular/common/http';
import { SignIn } from '@pages/sign-in/sign-in';

@Component({
  selector: 'app-sign-in-form',
  imports: [CardSkeleton, CustomInput, FormButton, ReactiveFormsModule, MatCheckbox],
  templateUrl: './sign-in-form.html',
  styleUrl: './sign-in-form.css',
})
export class SignInForm {
  private account = inject(AccountService);
  private router = inject(Router);
  private snackBar = inject(MatSnackBar);

  form = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
    rememberMe: new FormControl(true),
  });

  isLoading = signal(false);

  submit() {
    if (this.form.invalid) return;

    const value = this.form.value;

    this.isLoading.set(true);

    const signInRequest: SignInRequest = {
      email: value.email!,
      password: value.password!,
      rememberMe: value.rememberMe!,
    };

    this.account
      .signIn(signInRequest)
      .pipe(finalize(() => this.isLoading.set(false)))
      .subscribe({
        next: (res) => {
          this.router.navigate(['/dash']);
        },
        error: (err: HttpErrorResponse) => {
          if (err.status == 401) {
            this.snackBar.open('Incorrect username or password', 'CLOSE', {
              panelClass: ['toast-error'],
              duration: 5000,
            });
          } else {
            this.snackBar.open('Error when signing up', 'CLOSE', {
              panelClass: ['toast-error'],
              duration: 5000,
            });
          }
          console.error('Sign-in failed:', err);
        },
      });
  }
}
