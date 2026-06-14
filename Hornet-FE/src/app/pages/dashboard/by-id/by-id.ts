import { Component, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LaunchService } from '@services/core/launch/launch.service';
import { injectQuery } from '@tanstack/angular-query-experimental';
import { lastValueFrom } from 'rxjs';
import { toSignal } from '@angular/core/rxjs-interop';
import { LaunchFocus } from "@components/domain/launch/launch-focus/launch-focus";

@Component({
  selector: 'app-by-id',
  imports: [LaunchFocus],
  templateUrl: './by-id.html',
  styleUrl: './by-id.css',
})
export class ById {
private route = inject(ActivatedRoute);
  private launchService = inject(LaunchService);

  launchId = toSignal(
    this.route.paramMap,
    { initialValue: null }
  );

  launchQuery = injectQuery(() => {
    const id = this.launchId()?.get('id');

    return {
      queryKey: ['launch', id],
      enabled: !!id,
      queryFn: () =>
        lastValueFrom(this.launchService.getLaunch(id!)),
    };
  });

  get launch() {
    return this.launchQuery.data();
  }
}
