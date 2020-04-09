import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CoreModule } from './core.module';
import { Constants } from './Constants';
import { Observable } from 'rxjs';
import { Transfer } from './Entities/Transfer';
import { TransferLog } from './Entities/TransferLog';

@Injectable()
export class BankingService {
    // private readonly _tenantTeste: String = "497f79b8-1699-5764-2b8e-39f465435497";
    private readonly _tenantTeste: String = "abp";

    constructor(private httpClient: HttpClient) { }

    getAccounts(): Observable<Account[]>{
      return this.httpClient.get<Account[]>(Constants.uriWebGateway + `bankingservice/account?__tenant=${this._tenantTeste}`);
    }

    postTransfer(transfer : Transfer): Observable<any> {
        return this.httpClient.post<Transfer>(Constants.uriWebGateway + `bankingservice/account/transfer?__tenant=${this._tenantTeste}`, transfer);
    }

    getTransferLogs() {
        return this.httpClient.get<TransferLog[]>(Constants.uriWebGateway + `transferlogservice/transferLog​/transferLog?__tenant=${this._tenantTeste}`);
    }
}