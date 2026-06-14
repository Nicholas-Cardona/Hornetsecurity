import { Routes } from '@angular/router';
import { SignIn } from '@pages/sign-in/sign-in';
import { SignUp } from '@pages/sign-up/sign-up';
import { Dashboard } from '@pages/dashboard/dashboard';
import { Latest } from '@pages/dashboard/latest/latest';
import { Upcoming } from '@pages/dashboard/upcoming/upcoming';
import { Last } from '@pages/dashboard/last/last';
import { ById } from '@pages/dashboard/by-id/by-id';

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
    path: 'dash',
    component: Dashboard,
    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'last'
      },
      {
        path: "launch/:id",
        component: ById
      },
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
