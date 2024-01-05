import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from '@/presentation/app-routing.module';
import { AppComponent } from '@presentation/app.component';
import { HomeComponent } from '@presentation/home/home.component';
import { SharedModule } from '@presentation/shared/shared.module';
import { PlayComponent } from '@presentation/play/play.component';

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
