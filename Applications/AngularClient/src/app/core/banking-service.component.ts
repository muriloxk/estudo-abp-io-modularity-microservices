import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Constants } from './Constants';
import { Observable } from 'rxjs';
import { Transfer } from './Entities/Transfer';
import { TransferLog } from './Entities/TransferLog';
import { Account } from './Entities/Account';

@Injectable()
export class BankingService {

    constructor(private httpClient: HttpClient) { }

    getAccounts(): Observable<Account[]>{
      return this.httpClient.get<Account[]>(Constants.uriWebGateway + `bankingservice/account`);
    }

    postTransfer(transfer : Transfer): Observable<any> {
        transfer = new Transfer();
        transfer.FromAccount = "Murilo",
        transfer.ToAccount = "Teste",
        transfer.TransferAccount = 10

        return this.httpClient.post<Transfer>(Constants.uriWebGateway + `bankingservice/account/transfer`, transfer);
    }

    getTransferLogs() {
        return this.httpClient.get<TransferLog[]>(Constants.uriWebGateway + `transferlogservice/transferLog​/transferLog`);
    }
}