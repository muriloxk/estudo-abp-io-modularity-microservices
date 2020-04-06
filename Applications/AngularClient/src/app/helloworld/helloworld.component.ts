import { Component, OnInit, OnChanges } from '@angular/core';
import { BankingService } from '../core/banking-service.component';
import { AuthService } from '../core/auth-service.component';

@Component({
  selector: 'app-helloworld',
  templateUrl: './helloworld.component.html',
  styleUrls: ['./helloworld.component.css']
})
export class HelloworldComponent implements OnInit, OnChanges {

  private accounts : Account[];

  constructor(private bankingService: BankingService, 
              private authService: AuthService) { }

  ngOnInit(): void {
    console.log("ngOnChanges HelloWorldComponent")
    this.authService.isLoggedIn().then(loggedIn => {
      if(loggedIn){
        this.bankingService.getAccounts().subscribe(accounts => this.accounts = accounts);
        // Teste para verificar se está funcionando o identity server.
        console.log("Accounts", this.accounts);
      }    });
  
  }

  ngOnChanges(): void {
   

  }
}
