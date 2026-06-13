import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';

@Service()
export class AccountService {
  private httpClient = inject(HttpClient);

  private accountApiUrl(endpoint: string) {
    return `/api/account/${endpoint}`;
  }

  public signIn(request: SignInRequest) {
    return this.httpClient.post(this.accountApiUrl('sign-in'), {
      params: { email: request.email, password: request.password },
    });
  }

  public signUp(request: SignUpRequest) {
    return this.httpClient.post(this.accountApiUrl('sign-up'), request);
  }
}

export interface SignUpRequest {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  confirmPassword: string;
}

export interface SignInRequest {
  email: string;
  password: string;
}
