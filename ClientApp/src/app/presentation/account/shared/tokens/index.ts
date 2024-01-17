import { InjectionToken } from '@angular/core';
import {
  ILoginUseCase,
  IRegisterUseCase,
} from '@domain/models/interfaces/useCases';

export const IRegisterUseCaseToken = new InjectionToken<IRegisterUseCase>(
  'IRegisterUseCase'
);
export const ILoginUseCaseToken = new InjectionToken<ILoginUseCase>(
  'ILoginUseCase'
);
