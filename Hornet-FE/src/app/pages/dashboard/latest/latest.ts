import { Component, inject, signal } from '@angular/core';
import { LaunchService } from '@services/core/launch/launch.service';
import { injectQuery } from '@tanstack/angular-query-experimental';
import { lastValueFrom } from 'rxjs';
import { LaunchGridComponent } from "@components/domain/launch/launch-grid/launch-grid";
import { LaunchMode } from '@services/core/launch/LaunchMode';

@Component({
  selector: 'app-latest',
  imports: [LaunchGridComponent],
  templateUrl: './latest.html',
  styleUrl: './latest.css',
})
export class Latest {
  private launchService = inject(LaunchService);

  pagingSize = 8

  latestLaunchesPage = signal(1);

handleChangeLatestPage(p:number){
    this.latestLaunchesPage.set(p)
  }

  latestLaunches = injectQuery(() => ({
    queryKey: ['latest-launches', this.latestLaunchesPage()],
    queryFn: () =>
      lastValueFrom(
        this.launchService.getLatestLaunches({ page: this.latestLaunchesPage(), size: this.pagingSize }),
      )
  }));

 latestLaunchesCount = injectQuery(() => ({
    queryKey: ['upcoming-launches-count', 'past'],
    queryFn:()=>
      lastValueFrom(
        this.launchService.getLaunchesCount(LaunchMode.past)
      )
  }))
}
