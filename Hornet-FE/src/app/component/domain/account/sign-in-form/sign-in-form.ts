import { Component, inject } from '@angular/core';
import { CardSkeleton } from "../../../utils/card/card-skeleton/card-skeleton";
import { CustomInput } from '../../../utils/input/custom-input/custom-input';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FormButton } from "../../../utils/input/form-button/form-button";
import { AccountService, SignInRequest } from '../../../../services/core/account';
import { finalize } from 'rxjs';
import { MatCheckbox } from "@angular/material/checkbox";


@Component({
  selector: 'app-sign-in-form',
  imports: [CardSkeleton, CustomInput, FormButton, ReactiveFormsModule, MatCheckbox],
  templateUrl: './sign-in-form.html',
  styleUrl: './sign-in-form.css',
})
export class SignInForm {
  form = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
    rememberMe: new FormControl(true)
  });

  isLoading = false;
  account= inject(AccountService)

submit(){
    if (this.form.invalid) return;

    const value = this.form.value


    this.isLoading = true

    const signInRequest: SignInRequest = {
      email: value.email!,
      password: value.password!,
      rememberMe: value.rememberMe!
    }

    this.account.signIn(signInRequest).pipe(
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
