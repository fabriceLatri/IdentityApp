import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from '@presentation/account/login/login.component';
import { RegisterComponent } from '@presentation/account/register/register.component';
import { AccountRoutingModule } from '@presentation/account/account-routing.module';
import { SharedModule } from '@presentation/shared/shared.module';
import { LoginUseCase, RegisterUseCase } from '@domain/useCases';

/**
 * TODO: Create index.ts file in adapters folder
 */
import {
  AccountAdapter,
  IAccountPortToken,
} from '@infrastructure/adapters/account/account.adapter';
import { IAccountPort } from '@domain/ports/interfaces';
import { SharedServiceToken } from '@presentation/shared/services/injectionToken';
import { SharedService } from '@presentation/shared/services/shared.service';
import {
  ILoginUseCaseToken,
  IRegisterUseCaseToken,
} from '@presentation/account/shared/tokens';
@NgModule({
  declarations: [LoginComponent, RegisterComponent],
  imports: [CommonModule, AccountRoutingModule, SharedModule],
  providers: [
    {
      provide: SharedServiceToken,
      useClass: SharedService,
    },
    { provide: IAccountPortToken, useClass: AccountAdapter },
    {
      provide: ILoginUseCaseToken,
      useFactory: (accountAdapter: IAccountPort) =>
        new LoginUseCase(accountAdapter),
      deps: [IAccountPortToken],
    },
    {
      provide: IRegisterUseCaseToken,
      useFactory: (accountAdapter: IAccountPort) =>
        new RegisterUseCase(accountAdapter),
      deps: [IAccountPortToken],
    },
  ],
})
export class AccountModule {}
