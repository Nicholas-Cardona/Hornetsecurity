import { Component, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LaunchService } from '@services/core/launch/launch.service';
import { injectQuery } from '@tanstack/angular-query-experimental';
import { lastValueFrom } from 'rxjs';
import { toSignal } from '@angular/core/rxjs-interop';
import { LaunchFocus } from '@components/domain/launch/launch-focus/launch-focus';
import { Launch } from '@services/core/launch/Launch';

@Component({
  selector: 'app-by-id',
  imports: [LaunchFocus],
  templateUrl: './by-id.html',
  styleUrl: './by-id.css',
})
export class ById implements OnInit {
  private route = inject(ActivatedRoute);
  private launchService = inject(LaunchService);

  launchId = toSignal(this.route.paramMap, { initialValue: null });
  launch = signal<Launch | undefined>(undefined);

  async handleRefresh() {
    const id = this.launchId()?.get('id');

    if (!id) return;

    this.launchService.getLaunch(id).subscribe((val) => {
      this.launch.set(val);
    });
  }

  ngOnInit(): void {
    this.handleRefresh();
  }
}
