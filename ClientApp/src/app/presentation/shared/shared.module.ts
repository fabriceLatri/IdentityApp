import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

import { NotFoundComponent } from '@presentation/shared/components/errors/not-found/not-found.component';
import { ValidationMessagesComponent } from '@presentation/shared/components/errors/validation-messages/validation-messages.component';
import { NavbarComponent } from '@presentation/shared/layouts/navbar/navbar.component';
import { FooterComponent } from '@presentation/shared/layouts/footer/footer.component';

@NgModule({
  declarations: [
    NotFoundComponent,
    ValidationMessagesComponent,
    NavbarComponent,
    FooterComponent,
  ],
  imports: [CommonModule, RouterModule, ReactiveFormsModule, HttpClientModule],
  exports: [
    NavbarComponent,
    FooterComponent,
    NotFoundComponent,
    RouterModule,
    ReactiveFormsModule,
    HttpClientModule,
  ],
})
export class SharedModule {}
