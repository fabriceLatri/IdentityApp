import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from '@/account/login/login.component';
import { RegisterComponent } from '@/account/register/register.component';
import { AccountRoutingModule } from '@/account/account-routing.module';

@NgModule({
  declarations: [LoginComponent, RegisterComponent],
  imports: [CommonModule, AccountRoutingModule],
})
export class AccountModule {}
