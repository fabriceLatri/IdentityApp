import { Injectable, InjectionToken } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';

import { IAccountPort } from '@/domain/ports/account/account-port.interface';
import { IRegisterRequest } from '@/infrastructure/DTOs/requests/account/register';
import { RegisterDto } from '@/infrastructure/DTOs/responses/account/register.dto';

import { ApiConstant } from '@infrastructure/constants/api';
import { BaseAdapter } from '@infrastructure/adapters/base.adapter';
import { AccountErrorResponse } from '@/infrastructure/errors';
import { AccountRegisterEntity } from '@/domain/models/entities';
import { IAccountRegisterEntity } from '@/domain/models/interfaces';
import { RegisterEntity } from '@/infrastructure/models/entities/account/register.entity';

export const IAccountPortToken = new InjectionToken<IAccountPort>(
  'IAccountPort'
);

@Injectable()
export class AccountAdapter extends BaseAdapter implements IAccountPort {
  constructor(private readonly http: HttpClient) {
    super();
  }

  async register(register: IRegisterRequest): Promise<IAccountRegisterEntity> {
    try {
      const response$ = this.http.post<RegisterDto>(
        ApiConstant.registerUrl,
        register
      );

      const registerEntity: AccountRegisterEntity = this.mapTo<
        AccountRegisterEntity,
        RegisterDto
      >(RegisterEntity, await lastValueFrom(response$));

      return registerEntity;
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
