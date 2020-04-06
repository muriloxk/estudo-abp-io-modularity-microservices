import { Dictionary } from './IDictionary';

export class TransferLog {
   public FromAccount: String;
   public ToAccount: String;
   public TransferAmount: Number;
   public TenantId: String;
   public ExtraProperties: Dictionary<string> = {};
}