import { Inject, Injectable, InjectionToken } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';

import { IAccountPort, ICachePort } from '@domain/ports/interfaces';
import { IRegisterRequest } from '@domain/models/DTOs/requests/account/register';
import type { LoginDto, RegisterDto } from '@domain/models/DTOs/responses';

import {
  IAccountLoginEntity,
  IAccountRegisterEntity,
} from '@domain/models/interfaces';

import { ApiConstant } from '@infrastructure/constants/api';
import { BaseAdapter } from '@infrastructure/adapters/base.adapter';
import { AccountErrorResponse } from '@infrastructure/errors';
import { RegisterEntity } from '@infrastructure/models/entities/account/register.entity';
import { ILoginRequest } from '@domain/models/DTOs/requests';
import { LoginEntity } from '@infrastructure/models/entities/account/login.entity';
import { ICachePortToken } from '@presentation/shared/injectionTokens';
import { environment } from 'environments/environment.development';

export const IAccountPortToken = new InjectionToken<IAccountPort>(
  'IAccountPort'
);

@Injectable()
export class AccountAdapter extends BaseAdapter implements IAccountPort {
  //#region Ctor, Dtor
  constructor(
    @Inject(ICachePortToken) private readonly cacheAdapter: ICachePort,
    private readonly http: HttpClient
  ) {
    super();
  }
  //#endregion

  //#region Public Implementation Methods
  async login(login: ILoginRequest): Promise<IAccountLoginEntity> {
    try {
      const response$ = this.http.post<LoginDto>(ApiConstant.loginUrl, login);

      const response: LoginDto = await lastValueFrom(response$);

      // Add in storage
      this.setUser(response);

      const loginEntity: IAccountLoginEntity = this.mapTo<
        IAccountLoginEntity,
        LoginDto
      >(LoginEntity, response);

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

  //#region Private Methods
  private setUser(loginDto: LoginDto) {
    this.cacheAdapter.setCache(environment.userKey, JSON.stringify(loginDto));
  }
  //#endregion
}
