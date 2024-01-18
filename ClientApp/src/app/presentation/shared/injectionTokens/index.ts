// All injectionTokens in the general state of the app

import { InjectionToken } from '@angular/core';
import { ICachePort } from '@domain/ports/interfaces';

export const ICachePortToken = new InjectionToken<ICachePort>('ICachePort');
