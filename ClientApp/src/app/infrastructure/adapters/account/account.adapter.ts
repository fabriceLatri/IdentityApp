import { Injectable, InjectionToken } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';

import { IAccountPort } from '@/domain/ports/account/account-port.interface';
import { IRegisterRequest } from '@/domain/ports/DTOs/requests/account/register';
import { RegisterResponse } from '@/domain/ports/DTOs/responses/account/register';

import { ApiConstant } from '@infrastructure/constants/api';
import { BaseAdapter } from '@infrastructure/adapters/base.adapter';
import { AccountErrorResponse } from '@/infrastructure/errors';

export const IAccountPortToken = new InjectionToken<IAccountPort>(
  'IAccountPort'
);

@Injectable()
export class AccountAdapter extends BaseAdapter implements IAccountPort {
  constructor(private readonly http: HttpClient) {
    super();
  }

  async register(register: IRegisterRequest): Promise<string | never> {
    try {
      const response$ = this.http.post<RegisterResponse>(
        ApiConstant.registerUrl,
        register
      );

      const response: RegisterResponse = this.mapTo(
        RegisterResponse,
        await lastValueFrom(response$)
      );

      return response.message;
    } catch (err) {
      if (err instanceof HttpErrorResponse) {
        if (err.error.errors) {
          throw new AccountErrorResponse(err.message, err.error.errors);
        } else if (typeof err.error === 'string') {
          throw new AccountErrorResponse(err.message, [err.error]);
        } else {
          throw new AccountErrorResponse(err.message, [err.message]);
        }
      }
      throw new AccountErrorResponse('Unknown Error', ['Unknown Error']);
    }
  }
}
