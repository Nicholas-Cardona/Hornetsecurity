import { Component, inject, signal } from '@angular/core';
import { LaunchService } from '@services/core/launch/launch.service';
import { injectQuery } from '@tanstack/angular-query-experimental';
import { lastValueFrom } from 'rxjs';
import { NgOptimizedImage, NgStyle } from "@angular/common";

@Component({
  selector: 'app-last',
  imports: [NgStyle],
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

  formatDate(date?:DateString){
    return new Date(date ?? '').toLocaleDateString()
  }

  get launchNet(){
    return this.formatDate(this.launch?.net);
  }

  get windowStart(){
    return this.formatDate(this.launch?.windowStart)
  }
  
  get windowEnd(){
    return this.formatDate(this.launch?.windowEnd)
  }
}
