import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { SignInRequest } from './SignInRequest';
import { SignUpRequest } from './SignUpRequest';

@Service()
export class AccountService {
 private httpClient = inject(HttpClient);

  private accountApiUrl(endpoint: string) {
    return `/api/account/${endpoint}`;
  }

  public signIn(request: SignInRequest) {
    return this.httpClient.post(this.accountApiUrl('sign-in'), request);
  }

  public signUp(request: SignUpRequest) {
    return this.httpClient.post(this.accountApiUrl('sign-up'), request);
  }
}
