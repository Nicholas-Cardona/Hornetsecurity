import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { Launch } from '@services/core/launch/Launch';

@Component({
  selector: 'app-launch',
  imports: [CommonModule],
  templateUrl: './launch.html',
  styleUrl: './launch.css',
})
export class LaunchComponent {
  @Input({required: true}) launch!: Launch 
}
