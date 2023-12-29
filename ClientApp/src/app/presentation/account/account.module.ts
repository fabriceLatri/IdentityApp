import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from '@presentation/account/login/login.component';
import { RegisterComponent } from '@presentation/account/register/register.component';
import { AccountRoutingModule } from '@presentation/account/account-routing.module';
import { SharedModule } from '@presentation/shared/shared.module';
@NgModule({
  declarations: [LoginComponent, RegisterComponent],
  imports: [CommonModule, AccountRoutingModule, SharedModule],
})
export class AccountModule {}
