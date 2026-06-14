import { Component, inject } from '@angular/core';
import { LaunchService } from '@services/core/launch/launch.service';
import { injectQuery } from '@tanstack/angular-query-experimental';
import { lastValueFrom } from 'rxjs';
import { LaunchComponent } from "@components/domain/launch/launch/launch";

@Component({
  selector: 'app-dash',
  imports: [LaunchComponent],
  templateUrl: './dash.html',
  styleUrl: './dash.css',
})
export class Dash {
    private launchService = inject(LaunchService);

  query = injectQuery(() => ({
    queryKey: ['latest-launches'],
    queryFn: () => 
      lastValueFrom(
        this.launchService.getLatestLaunches({ page: 1, size: 10 }),
      )
  }));
}
