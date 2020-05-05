import { Component, OnInit } from '@angular/core';
import { AuthService } from './core/auth-service.component';
import { BankingService } from './core/banking-service.component';
import { Observable } from 'rxjs';
import { Account } from './core/Entities/Account';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'AngularClient';
  isLoggedIn = false;
  accounts = null;


  constructor(private _authService: AuthService,
              private _bankingService: BankingService) {}
  
  ngOnInit() {
    this._authService.isLoggedIn().then(loggedIn => {
      this.isLoggedIn = loggedIn;
    });
  }

  getList() {
    this._bankingService.getAccounts()
                        .subscribe(accounts => this.accounts = accounts,
                                   err => console.error('Observer got an error: ' + err));
  }

  postTransfer() {
    this._bankingService.postTransfer(null)
                        .subscribe(response => console.log("Transfer enviado com sucesso"),
                                   err => console.log("Erro ao enviar o transfer..."));
  }

  login() {
    this._authService.login();
  }

  logout() {
    this._authService.logout();
  }
}
