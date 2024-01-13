import { InjectionToken } from '@angular/core';
import { ISharedService } from '@presentation/shared/services/interfaces/shared-service.interface';

export const SharedServiceToken = new InjectionToken<ISharedService>(
  'ISharedService'
);
