import { Component, effect, inject, OnInit, signal } from '@angular/core';
import { LaunchService } from '@services/core/launch/launch.service';
import { LaunchGridComponent } from '@components/domain/launch/launch-grid/launch-grid';
import { LaunchMode } from '@services/core/launch/LaunchMode';
import { Launch } from '@services/core/launch/Launch';

@Component({
  selector: 'app-latest',
  imports: [LaunchGridComponent],
  templateUrl: './latest.html',
  styleUrl: './latest.css',
})
export class Latest implements OnInit {
  private launchService = inject(LaunchService);

  pagingSize = 8;

  latestLaunchesPage = signal(1);

  handleChangeLatestPage(p: number) {
    this.latestLaunchesPage.set(p);
  }

  latestLaunches = signal<Launch[] | undefined>(undefined);
  latestLaunchesCount = signal<number | undefined>(undefined);

  handleFetchLatest() {
    this.launchService
      .getLatestLaunches({ page: this.latestLaunchesPage(), size: this.pagingSize })
      .subscribe((val) => {
        this.latestLaunches.set(val);
      });
  }

  handleFetchCount() {
    this.launchService.getLaunchesCount(LaunchMode.past).subscribe((val) => {
      this.latestLaunchesCount.set(val);
    });
  }

  constructor() {
    effect(() => {
      const page = this.latestLaunchesPage();

      this.handleFetchLatest();
    });
  }

  ngOnInit(): void {
    this.handleFetchCount();
    this.handleFetchLatest();
  }
}
