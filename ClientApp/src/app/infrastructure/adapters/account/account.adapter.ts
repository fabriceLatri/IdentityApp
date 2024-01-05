import { Injectable, InjectionToken } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { map } from 'rxjs/operators';

import { IAccountPort } from '@/domain/ports/account/account-port.interface';
import { IRegisterRequest } from '@/domain/ports/DTOs/requests/account/register';
import { IRegisterResponse } from '@/domain/ports/DTOs/responses/account/register';
import { ApiConstant } from '@infrastructure/constants/api';

export const IAccountPortToken = new InjectionToken<IAccountPort>(
  'IAccountPort'
);

@Injectable()
export class AccountAdapter implements IAccountPort {
  private _errorMessages: string[] = [];

  constructor(private readonly http: HttpClient) {}

  public get errorMessages(): string[] {
    return this._errorMessages;
  }

  register(register: IRegisterRequest): void {
    this.http
      .post<IRegisterResponse>(ApiConstant.registerUrl, register)
      .subscribe({
        next: (value) => {
          console.log(value.message);
        },
        error: (err: HttpErrorResponse) => {
          if (err.error.errors) {
            this._errorMessages = err.error.errors;
          }
        },
      });
  }
}
