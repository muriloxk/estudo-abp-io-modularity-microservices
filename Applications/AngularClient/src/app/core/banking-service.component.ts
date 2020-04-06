import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CoreModule } from './core.module';
import { Constants } from './Constants';
import { Observable } from 'rxjs';
import { Transfer } from './Entities/Transfer';
import { TransferLog } from './Entities/TransferLog';

@Injectable()
export class BankingService {

    constructor(private httpClient: HttpClient) { }

    getAccounts(): Observable<Account[]>{
      return this.httpClient.get<Account[]>(Constants.uriWebGateway + "bankingservice/account");
    }

    postTransfer(transfer : Transfer): Observable<any> {
        return this.httpClient.post<Transfer>(Constants.uriWebGateway + "bankingservice/account/transfer", transfer);
    }

    getTransferLogs() {
        return this.httpClient.get<TransferLog[]>(Constants.uriWebGateway + "transferlogservice/transferLog​/transferLog");
    }
}