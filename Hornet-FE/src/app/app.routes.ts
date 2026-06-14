import { Routes } from '@angular/router';
import { SignIn } from '@pages/sign-in/sign-in';
import { SignUp } from '@pages/sign-up/sign-up';
import { Home } from '@pages/home/home';
import { Dash } from '@pages/dash/dash';

export const routes: Routes = [
  {
    path: 'sign-in',
    component: SignIn,
  },
  {
    path: 'sign-up',
    component: SignUp,
  },
  {
    path: '',
    component: Home,
  },
  {
    path: "dash",
    component: Dash
  }
];
