import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';

@Service()
export class LaunchesService {
    private httpClient = inject(HttpClient)

  private accountApiUrl(endpoint: string) {
    return `/api/account/${endpoint}`;
  }

    public getLatestLaunches(){
        this.httpClient.post(this.accountApiUrl("latest"), {})
    }
}
