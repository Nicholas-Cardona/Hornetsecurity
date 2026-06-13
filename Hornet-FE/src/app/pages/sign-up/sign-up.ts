import { Component } from '@angular/core';
import { SignUpForm } from "../../component/domain/account/sign-up-form/sign-up-form";
import { RouterLink } from "@angular/router";

@Component({
  selector: 'app-sign-up',
  imports: [SignUpForm, RouterLink],
  templateUrl: './sign-up.html',
  styleUrl: './sign-up.css',
})
export class SignUp {}
