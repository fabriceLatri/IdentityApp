import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from '@presentation/account/login/login.component';
import { RegisterComponent } from '@presentation/account/register/register.component';
import { AccountRoutingModule } from '@presentation/account/account-routing.module';
import { SharedModule } from '@presentation/shared/shared.module';
import { AccountUseCase } from '@domain/useCases/account/account.use-case';
import {
  AccountAdapter,
  IAccountPortToken,
} from '@infrastructure/adapters/account/account.adapter';
import { IAccountPort } from '@/domain/ports/account/account-port.interface';
@NgModule({
  declarations: [LoginComponent, RegisterComponent],
  imports: [CommonModule, AccountRoutingModule, SharedModule],
  providers: [
    { provide: IAccountPortToken, useClass: AccountAdapter },
    {
      provide: AccountUseCase,
      useFactory: (accountAdapter: IAccountPort) =>
        new AccountUseCase(accountAdapter),
      deps: [IAccountPortToken],
    },
  ],
})
export class AccountModule {}
