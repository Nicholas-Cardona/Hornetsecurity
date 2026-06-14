import { Routes } from '@angular/router';
import { SignIn } from '@pages/sign-in/sign-in';
import { SignUp } from '@pages/sign-up/sign-up';
import { Dash } from '@pages/dash/dash';
import { Dashboard } from '@pages/dashboard/dashboard';
import { Latest } from '@pages/dashboard/latest/latest';
import { Upcoming } from '@pages/dashboard/upcoming/upcoming';
import { Last } from '@pages/dashboard/last/last';

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
    pathMatch: 'full',
    redirectTo: '/dash'
  },
  {
    path: "dash",
    component: Dashboard,
    children: [
      {
        path: 'latest',
        component: Latest
      },
      {
        path: 'upcoming',
        component: Upcoming
      },
      {
        path: 'last',
        component: Last
      }
    ]
  },
];
