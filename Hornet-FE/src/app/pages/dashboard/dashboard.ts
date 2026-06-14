import { Component, signal } from '@angular/core';
import { RouterOutlet, RouterLinkActive } from "@angular/router";
import { RouterLinkWithHref } from "@angular/router";

@Component({
  selector: 'app-dashboard',
  imports: [RouterOutlet, RouterLinkWithHref, RouterLinkActive],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard {
  navLinks = signal([
    { path: '/dash/latest', label: 'Latest', exact: true },
    { path: '/dash/upcoming', label: 'Upcoming', exact: false },
    { path: '/dash/last', label: 'Last', exact: false }
  ]);
}
