import { Injectable, InjectionToken } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';

import { IAccountPort } from '@domain/ports/interfaces';
import { IRegisterRequest } from '@domain/models/DTOs/requests/account/register';
import type { LoginDto, RegisterDto } from '@domain/models/DTOs/responses';

import {
  IAccountLoginEntity,
  IAccountRegisterEntity,
} from '@domain/models/interfaces';
import {
  AccountLoginEntity,
  AccountRegisterEntity,
} from '@domain/models/entities';

import { ApiConstant } from '@infrastructure/constants/api';
import { BaseAdapter } from '@infrastructure/adapters/base.adapter';
import { AccountErrorResponse } from '@infrastructure/errors';
import { RegisterEntity } from '@infrastructure/models/entities/account/register.entity';
import { ILoginRequest } from '@domain/models/DTOs/requests';
import { LoginEntity } from '@infrastructure/models/entities/account/login.entity';

export const IAccountPortToken = new InjectionToken<IAccountPort>(
  'IAccountPort'
);

@Injectable()
export class AccountAdapter extends BaseAdapter implements IAccountPort {
  //#region Ctor, Dtor
  constructor(private readonly http: HttpClient) {
    super();
  }
  //#endregion

  //#region Public Implementation Methods
  async login(login: ILoginRequest): Promise<IAccountLoginEntity> {
    try {
      const response$ = this.http.post<LoginDto>(ApiConstant.loginUrl, login);

      const loginEntity: IAccountLoginEntity = this.mapTo<
        IAccountLoginEntity,
        LoginDto
      >(LoginEntity, await lastValueFrom(response$));

      return loginEntity;
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

  async register(register: IRegisterRequest): Promise<IAccountRegisterEntity> {
    try {
      const response$ = this.http.post<RegisterDto>(
        ApiConstant.registerUrl,
        register
      );

      const registerEntity: IAccountRegisterEntity = this.mapTo<
        IAccountRegisterEntity,
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
  //#endregion
}
