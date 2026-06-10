import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';

@Service()
export class Account {
  private httpClient = inject(HttpClient);

  private accountApiUrl(endpoint: string) {
    return `/api/account/${endpoint}`;
  }

  public signIn(request: SignInRequest) {
    this.httpClient.get(this.accountApiUrl('/sign-in'), {
      params: { email: request.email, password: request.password },
    });
  }

  public signUp(request: SignUpRequest) {
    this.httpClient.post(this.accountApiUrl('/'), request);
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
