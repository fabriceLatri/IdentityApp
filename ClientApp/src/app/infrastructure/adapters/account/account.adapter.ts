import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, ReplaySubject, lastValueFrom } from 'rxjs';

import { IAccountPort, ICachePort } from '@domain/ports/interfaces';
import { IRegisterRequest } from '@domain/models/DTOs/requests/account/register';
import type { LoginDto, RegisterDto } from '@domain/models/DTOs/responses';

import { IUser, IAccountRegisterEntity } from '@domain/models/interfaces';

import { ApiConstant } from '@infrastructure/constants/api';
import { BaseAdapter } from '@infrastructure/adapters/base.adapter';
import { AccountErrorResponse } from '@infrastructure/errors';
import { RegisterEntity } from '@infrastructure/models/entities/account/register.entity';
import { ILoginRequest } from '@domain/models/DTOs/requests';
import { User } from '@infrastructure/models/entities/user/user.entity';
import { ICachePortToken } from '@presentation/shared/injectionTokens';
import { environment } from 'environments/environment.development';

@Injectable({ providedIn: 'root' })
export class AccountAdapter
  extends BaseAdapter
  implements IAccountPort<Observable<IUser | null>>
{
  //#region Private Fields
  private userSource = new ReplaySubject<IUser | null>();
  //#endregion

  //#region Public Fields
  user = this.userSource.asObservable();
  //#endregion

  //#region Ctor, Dtor
  constructor(
    @Inject(ICachePortToken) private readonly cacheAdapter: ICachePort,
    private readonly http: HttpClient
  ) {
    super();
  }
  //#endregion

  //#region Public Implementation Methods
  async login(login: ILoginRequest): Promise<IUser> {
    try {
      const response$ = this.http.post<LoginDto>(ApiConstant.loginUrl, login);

      const response: LoginDto = await lastValueFrom(response$);

      const userEntity: IUser = this.mapTo<IUser, LoginDto>(User, response);

      // Add in storage
      this.setUser(userEntity);

      return userEntity;
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
  private setUser(userEntity: IUser) {
    this.cacheAdapter.setCache(environment.userKey, JSON.stringify(userEntity));
    this.userSource.next(userEntity);
  }
  //#endregion
}
