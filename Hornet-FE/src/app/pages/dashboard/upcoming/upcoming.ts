import { Component, effect, inject, signal } from '@angular/core';
import { LaunchService } from '@services/core/launch/launch.service';
import { LaunchGridComponent } from '@components/domain/launch/launch-grid/launch-grid';
import { LaunchMode } from '@services/core/launch/LaunchMode';
import { Launch } from '@services/core/launch/Launch';

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
  launches = signal<Launch[] | undefined>(undefined);
  launchesCount = signal<number>(0);

  handleChangeUpcomingPage(p: number) {
    this.upcomingLauncesPage.set(p);
  }

  handleFetchLaunches() {
    this.launchService
      .getUpcomingLaunches({ page: this.upcomingLauncesPage(), size: this.pagingSize })
      .subscribe((val) => {
        this.launches.set(val);
      });
  }

  handleFetchCount() {
    this.launchService.getLaunchesCount(LaunchMode.upcoming).subscribe((val) => {
      this.launchesCount.set(val);
    });
  }

  constructor() {
    this.handleFetchCount();
    this.handleFetchLaunches();

    effect(() => {
      const currentPage = this.upcomingLauncesPage();

      this.handleFetchLaunches();
    });
  }
}
