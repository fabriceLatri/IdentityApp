import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from '@/app-routing.module';
import { AppComponent } from '@/app.component';
import { HomeComponent } from '@/home/home.component';
import { SharedModule } from '@/shared/shared.module';
import { PlayComponent } from './play/play.component';

@NgModule({
  declarations: [AppComponent, HomeComponent, PlayComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    SharedModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
