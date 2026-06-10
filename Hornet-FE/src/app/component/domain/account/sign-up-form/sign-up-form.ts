import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Account } from '../../../../services/core/account';
import { InputComponent } from "../../../input/text-input/text-input";

@Component({
  selector: 'app-sign-up-form',
  imports: [ReactiveFormsModule, InputComponent],
  templateUrl: './sign-up-form.html',
  styleUrl: './sign-up-form.css',
})
export class SignIn {
  private account = inject(Account);

  form = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
  });

  submit() {
    if (this.form.invalid) return;

    this.account.signIn(this.form.value as any).subscribe({
      next: (res) => {
        console.log('Logged in:', res);
      },
      error: (err) => {
        console.error('Login failed:', err);
      },
    });
  }
}
