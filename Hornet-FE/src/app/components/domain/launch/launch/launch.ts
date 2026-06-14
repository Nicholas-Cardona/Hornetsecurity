import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { Launch } from '@services/core/launch/Launch';
import { RouterLink } from "@angular/router";

@Component({
  selector: 'app-launch',
  imports: [CommonModule, RouterLink],
  templateUrl: './launch.html',
  styleUrl: './launch.css',
})
export class LaunchComponent {
  @Input({required:false }) launch: Launch | undefined = undefined
}
