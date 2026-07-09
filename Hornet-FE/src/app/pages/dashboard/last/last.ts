import { Component, inject, signal } from '@angular/core';
import { LaunchService } from '@services/core/launch/launch.service';
import { lastValueFrom } from 'rxjs';
import { LaunchFocus } from '@components/domain/launch/launch-focus/launch-focus';
import { Launch } from '@services/core/launch/Launch';

@Component({
  selector: 'app-last',
  imports: [LaunchFocus],
  templateUrl: './last.html',
  styleUrl: './last.css',
})
export class Last {
  private launchService = inject(LaunchService);
  launch = signal<Launch | undefined>(undefined);

  constructor() {
    this.handleGetLastLaunch();
  }

  handleGetLastLaunch() {
    this.launchService.getLastLaunch().subscribe((val) => {
      this.launch.set(val);
    });
  }
}
