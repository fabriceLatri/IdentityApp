import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotFoundComponent } from '@/shared/components/errors/not-found/not-found.component';
import { ValidationMessagesComponent } from '@/shared/components/errors/validation-messages/validation-messages.component';
import { NavbarComponent } from '@/shared/layouts/navbar/navbar.component';
import { FooterComponent } from '@/shared/layouts/footer/footer.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    NotFoundComponent,
    ValidationMessagesComponent,
    NavbarComponent,
    FooterComponent,
  ],
  imports: [CommonModule, RouterModule],
  exports: [NavbarComponent, FooterComponent, NotFoundComponent, RouterModule],
})
export class SharedModule {}
