import { Component, inject, signal } from '@angular/core';
import { LaunchService } from '@services/core/launch/launch.service';
import { injectQuery } from '@tanstack/angular-query-experimental';
import { lastValueFrom } from 'rxjs';
import { LaunchFocus } from "@components/domain/launch/launch-focus/launch-focus";

@Component({
  selector: 'app-last',
  imports: [LaunchFocus],
  templateUrl: './last.html',
  styleUrl: './last.css',
})
export class Last {
  private launchService = inject(LaunchService);

  launchQuery = injectQuery(() => ({
    queryKey: ['last-launch'],
    queryFn: () =>
      lastValueFrom(
        this.launchService.getLastLaunch(),
      )
  }));

  get launch(){
    return this.launchQuery.data()
  }
}
