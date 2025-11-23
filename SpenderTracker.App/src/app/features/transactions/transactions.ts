import { Component } from '@angular/core';
import { TransactionList } from './components/transaction-list/transaction-list';

@Component({
  selector: 'app-transactions',
  imports: [TransactionList],
  templateUrl: './transactions.html',
  styleUrl: './transactions.css',
})
export class Transactions {

}
