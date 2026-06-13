import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { InputComponent } from '../../component/utils/input/text-input/text-input';

@Component({
  selector: 'app-sign-in',
  imports: [RouterLink, InputComponent],
  templateUrl: './sign-in.html',
  styleUrl: './sign-in.css',
})
export class SignIn {
  value1 = '';
}
