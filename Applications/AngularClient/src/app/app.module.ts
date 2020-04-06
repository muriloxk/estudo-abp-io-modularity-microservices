import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HelloworldComponent } from './helloworld/helloworld.component';
import { SigninRedirectComponent } from './home/signin-redirect-callback.component';
import { SignoutRedirectComponent } from './home/signout-redirect-callback.component';
import { AuthService } from './core/auth-service.component';
import { HttpClient } from '@angular/common/http';
import { BankingService } from './core/banking-service.component';
import { CoreModule } from './core/core.module';

@NgModule({
  declarations: [
    AppComponent,
    HelloworldComponent,
    SigninRedirectComponent,
    SignoutRedirectComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CoreModule,
  ],
  providers: [
    AuthService,
    BankingService,
    HttpClient 
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
