import { Component, inject, signal } from '@angular/core';
import { LaunchService } from '@services/core/launch/launch.service';
import { injectQuery } from '@tanstack/angular-query-experimental';
import { lastValueFrom } from 'rxjs';
import { LaunchGridComponent } from "@components/domain/launch/launch-grid/launch-grid";
import { LaunchMode } from '@services/core/launch/LaunchMode';

@Component({
  selector: 'app-upcoming',
  imports: [LaunchGridComponent],
  templateUrl: './upcoming.html',
  styleUrl: './upcoming.css',
})
export class Upcoming {
  private launchService = inject(LaunchService);

  pagingSize = 8;

  upcomingLauncesPage = signal(1);

  handleChangeUpcomingPage(p: number) {
    this.upcomingLauncesPage.set(p)
  }

  upcomingLaunches = injectQuery(() => ({
    queryKey: ['upcoming-launches', this.upcomingLauncesPage()],
    queryFn: () =>
      lastValueFrom(
        this.launchService.getUpcomingLaunches({ page: this.upcomingLauncesPage(), size: this.pagingSize }),
      )
  }));

  upcomingLaunchesCount = injectQuery(() => ({
    queryKey: ['upcoming-launches-count', 'upcoming'],
    queryFn: () =>
      lastValueFrom(
        this.launchService.getLaunchesCount(LaunchMode.upcoming)
      )

  }))
}
