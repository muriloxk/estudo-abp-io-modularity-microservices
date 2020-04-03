import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HelloworldComponent } from './helloworld/helloworld.component';
import { SigninRedirectComponent } from './home/signin-redirect-callback.component';
import { SignoutRedirectComponent } from './home/signout-redirect-callback.component';

const routes: Routes = [
          {
            path: '**',
            component: HelloworldComponent
          },

          {
            path: 'signin-callback',
            component: SigninRedirectComponent
          },

          {
            path: 'signout-callback',
            component: SignoutRedirectComponent
          }
        ];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
