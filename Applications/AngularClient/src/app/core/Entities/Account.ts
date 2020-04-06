import { Dictionary } from './IDictionary';

export class Account {
    public AccountType : String;
    public AccountBalance : Number;
    public TenantId : String;
    public ExtraProperties :  Dictionary<string> = {};
    public ConcurrencyStamp : String;
    public Id : String;
}

