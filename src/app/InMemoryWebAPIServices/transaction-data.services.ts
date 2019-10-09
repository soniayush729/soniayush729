import { Injectable } from '@angular/core';
import { InMemoryDbService } from 'angular-in-memory-web-api';
import { Transaction } from '../Models/transaction';

@Injectable({
  providedIn: 'root'
})
export class TransactionDataService implements InMemoryDbService {
  constructor() { }
    createDb() {
      let transactions = [
        new Transaction(1, 404040, "401476EE-0A3B-482E-BD5B-B94A32355959", "Credit", 12000, "C628855C-FE7A-4D94-A1BB-167157D3F4EA", "10/3/2019", "Deposit Slip", ""),

        new Transaction(2, 305640, "6D68849C-8FA8-4049-A111-B431C76C6548", "Debit", 12000, "53E8748F-61D6-494B-BF72-E18B27511EFA", "10/3/2019", "Cheque", "234123"),

        new Transaction(3, 513141, "E9D83ECC-65EE-482A-860C-82A2A4E44F07", "Debit", 12000, "324A20CB-3027-48D7-BFFD-DA3E2AAC77EC", "10/3/2019", "Withdrawal Slip", ""),

        new Transaction(4, 405770, "F7533DB4-3CA5-468F-BAD5-32FC26B4E158", "Credit", 12000, "645BDD67-38D7-4C18-9208-904FBF4E28B4", "10/3/2019", "Cheque", "435812")
      ];
      return { transactions };
    }
  }

