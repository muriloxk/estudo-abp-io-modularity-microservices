import { Component, OnInit } from '@angular/core';
import { AuthService } from './core/auth-service.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'AngularClient';
  isLoggedIn = false;

  constructor(private _authService: AuthService) {}


  ngOnInit() {

    console.log("TEY TEY NG ON INIT!")
    this._authService.isLoggedIn().then(loggedIn => {
      console.log(loggedIn);
      this.isLoggedIn = loggedIn;
    });
  }

  login() {
    this._authService.login();
  }

  logout() {
    this._authService.logout();
  }
}
