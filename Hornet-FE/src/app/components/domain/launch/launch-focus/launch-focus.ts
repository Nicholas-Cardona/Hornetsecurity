import { Component, Input } from '@angular/core';
import { Launch } from '@services/core/launch/Launch';
import { NgOptimizedImage, NgStyle } from '@angular/common';

@Component({
  selector: 'app-launch-focus',
  imports: [NgStyle],
  templateUrl: './launch-focus.html',
  styleUrl: './launch-focus.css',
})
export class LaunchFocus {
  @Input() launch: Launch | undefined = undefined;

  formatDate(date?: DateString) {
    return new Date(date ?? '').toLocaleDateString();
  }

  get launchNet() {
    return this.formatDate(this.launch?.net);
  }

  get windowStart() {
    return this.formatDate(this.launch?.windowStart);
  }

  get windowEnd() {
    return this.formatDate(this.launch?.windowEnd);
  }
}
