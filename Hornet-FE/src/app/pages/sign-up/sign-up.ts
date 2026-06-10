import { Component } from '@angular/core';
import { SignIn } from "../../component/domain/account/sign-up-form/sign-up-form";

@Component({
  selector: 'app-sign-up',
  imports: [SignIn],
  templateUrl: './sign-up.html',
  styleUrl: './sign-up.css',
})
export class SignUp {}
