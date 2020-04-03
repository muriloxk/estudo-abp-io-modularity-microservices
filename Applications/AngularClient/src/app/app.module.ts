import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HelloworldComponent } from './helloworld/helloworld.component';
import { SigninRedirectComponent } from './home/signin-redirect-callback.component';
import { SignoutRedirectComponent } from './home/signout-redirect-callback.component';

@NgModule({
  declarations: [
    AppComponent,
    HelloworldComponent,
    SigninRedirectComponent,
    SignoutRedirectComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
