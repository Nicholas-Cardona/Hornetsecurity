import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { GetLatestLaunchesRequest } from './GetLatestLaunchesRequest';
import { LaunchMode } from './LaunchMode';
import { GetUpcomingLaunchesRequest } from './GetUpcomingLaunchesRequest';

@Service()
export class LaunchService {
  private httpClient = inject(HttpClient)

  private accountApiUrl(endpoint: string) {
    return `/api/account/${endpoint}`;
  }

  public getLatestLaunches(request: GetLatestLaunchesRequest) {
    return this.httpClient.get(this.accountApiUrl("latest"), { params: { page: request.page, size: request.size } })
  }

  public getLaunchesCount(launchMode: LaunchMode) {
    return this.httpClient.get(this.accountApiUrl("count"), { params: { mode: launchMode } })
  }

  public getUpcomingLaunches(request: GetUpcomingLaunchesRequest) {
    return this.httpClient.get(this.accountApiUrl("upcoming"), { params: { page: request.page, size: request.size } })
  }

  public getLastLaunch() {
    return this.httpClient.get(this.accountApiUrl("last"))
  }
}
