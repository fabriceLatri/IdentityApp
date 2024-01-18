import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from '@presentation/account/login/login.component';
import { RegisterComponent } from '@presentation/account/register/register.component';
import { AccountRoutingModule } from '@presentation/account/account-routing.module';
import { SharedModule } from '@presentation/shared/shared.module';
import { LoginUseCase, RegisterUseCase } from '@domain/useCases';
import { Observable } from 'rxjs';

/**
 * TODO: Create index.ts file in adapters folder
 */
import { IAccountPort } from '@domain/ports/interfaces';
import { SharedServiceToken } from '@presentation/shared/services/injectionToken';
import { SharedService } from '@presentation/shared/services/shared.service';

import { IUser } from '@domain/models/interfaces';
import {
  IAccountPortToken,
  ILoginUseCaseToken,
  IRegisterUseCaseToken,
} from '@presentation/shared/injectionTokens';
import { AccountAdapter } from '@infrastructure/adapters/account/account.adapter';
@NgModule({
  declarations: [LoginComponent, RegisterComponent],
  imports: [CommonModule, AccountRoutingModule, SharedModule],
  providers: [
    {
      provide: SharedServiceToken,
      useClass: SharedService,
    },
    { provide: IAccountPortToken, useExisting: AccountAdapter },
    {
      provide: ILoginUseCaseToken,
      useFactory: (accountAdapter: IAccountPort<Observable<IUser | null>>) =>
        new LoginUseCase(accountAdapter),
      deps: [IAccountPortToken],
    },
    {
      provide: IRegisterUseCaseToken,
      useFactory: (accountAdapter: IAccountPort<Observable<IUser | null>>) =>
        new RegisterUseCase(accountAdapter),
      deps: [IAccountPortToken],
    },
  ],
})
export class AccountModule {}
