import { Component, inject, signal } from '@angular/core';
import { RouterOutlet, RouterLinkActive } from '@angular/router';
import { RouterLinkWithHref } from '@angular/router';
import { AccountService, GetIdentityResponse } from '@services/core/account';

@Component({
  selector: 'app-dashboard',
  imports: [RouterOutlet, RouterLinkWithHref, RouterLinkActive],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard {
  private accountService = inject(AccountService);
  public userResponse = signal<GetIdentityResponse | null>(null);

  constructor() {
    this.load();
  }

  navLinks = signal([
    { path: '/dash/last', label: 'Last', exact: true },
    { path: '/dash/latest', label: 'Latest', exact: true },
    { path: '/dash/upcoming', label: 'Upcoming', exact: false },
  ]);

  async load() {
    this.accountService.getIdentity().subscribe((i) => {
      this.userResponse.set(i);
    });
  }
}
