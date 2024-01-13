import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

import { ModalModule } from 'ngx-bootstrap/modal';

import { NotFoundComponent } from '@presentation/shared/components/errors/not-found/not-found.component';
import { ValidationMessagesComponent } from '@presentation/shared/components/errors/validation-messages/validation-messages.component';
import { NavbarComponent } from '@presentation/shared/layouts/navbar/navbar.component';
import { FooterComponent } from '@presentation/shared/layouts/footer/footer.component';
import { NotificationComponent } from '@presentation/shared/components/modals/notification/notification.component';

@NgModule({
  declarations: [
    NotFoundComponent,
    ValidationMessagesComponent,
    NavbarComponent,
    FooterComponent,
    NotificationComponent,
  ],
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule,
    HttpClientModule,
    ModalModule.forRoot(),
  ],
  exports: [
    NavbarComponent,
    FooterComponent,
    NotFoundComponent,
    RouterModule,
    ReactiveFormsModule,
    HttpClientModule,
    ValidationMessagesComponent,
  ],
})
export class SharedModule {}
