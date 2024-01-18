// All injectionTokens in the general state of the app

import { InjectionToken } from '@angular/core';
import { IUser } from '@domain/models/interfaces';
import {
  IRegisterUseCase,
  ILoginUseCase,
} from '@domain/models/interfaces/useCases';
import { IAccountPort, ICachePort } from '@domain/ports/interfaces';
import { Observable } from 'rxjs';

export const ICachePortToken = new InjectionToken<ICachePort>('ICachePort');

export const IRegisterUseCaseToken = new InjectionToken<IRegisterUseCase>(
  'IRegisterUseCase'
);
export const ILoginUseCaseToken = new InjectionToken<ILoginUseCase>(
  'ILoginUseCase'
);
export const IAccountPortToken = new InjectionToken<
  IAccountPort<Observable<IUser | null>>
>('IAccountPort');
