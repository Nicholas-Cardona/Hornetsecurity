import { Component, inject, signal } from '@angular/core';
import { LaunchService } from '@services/core/launch/launch.service';
import { injectQuery } from '@tanstack/angular-query-experimental';
import { lastValueFrom } from 'rxjs';
import { LaunchGridComponent } from "@components/domain/launch/launch-grid/launch-grid";
import { LaunchMode } from '@services/core/launch/LaunchMode';

@Component({
  selector: 'app-dash',
  imports: [LaunchGridComponent],
  templateUrl: './dash.html',
  styleUrl: './dash.css',
})
export class Dash {
  private launchService = inject(LaunchService);

  pagingSize = 8

  latestLaunchesPage = signal(1);
  upcomingLauncesPage = signal(1);

  handleChangeUpcomingPage(p:number){
    this.upcomingLauncesPage.set(p)
  }

  latesetLaunches = injectQuery(() => ({
    queryKey: ['latest-launches', this.latestLaunchesPage()],
    queryFn: () =>
      lastValueFrom(
        this.launchService.getLatestLaunches({ page: this.latestLaunchesPage(), size: this.pagingSize }),
      )
  }));

  upcomingLaunches = injectQuery(() => ({
    queryKey: ['upcoming-launches', this.upcomingLauncesPage()],
    queryFn: () =>
      lastValueFrom(
        this.launchService.getUpcomingLaunches({ page: this.upcomingLauncesPage(), size: this.pagingSize }),
      )
  }));

  upcomingLaunchesCount = injectQuery(() => ({
    queryKey: ['upcoming-launches-count'],
    queryFn:()=>
      lastValueFrom(
        this.launchService.getLaunchesCount(LaunchMode.upcoming)
      )
    
  }))

   latesetLaunchesCount = injectQuery(() => ({
    queryKey: ['upcoming-launches-count'],
    queryFn:()=>
      lastValueFrom(
        this.launchService.getLaunchesCount(LaunchMode.upcoming)
      )
  }))
}
