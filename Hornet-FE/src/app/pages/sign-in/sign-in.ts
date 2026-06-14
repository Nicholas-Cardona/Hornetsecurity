import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { SignInForm } from "../../component/domain/account/sign-in-form/sign-in-form";

@Component({
  selector: 'app-sign-in',
  imports: [RouterLink, SignInForm],
  templateUrl: './sign-in.html',
  styleUrl: './sign-in.css',
})
export class SignIn {
}
