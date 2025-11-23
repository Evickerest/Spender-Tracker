import { Component, inject } from '@angular/core';
import { APIService } from '../../../../shared/services/apiservice';
import { Transaction } from '../../models/transaction';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-transaction-list',
  imports: [AsyncPipe],
  templateUrl: './transaction-list.html',
  styleUrl: './transaction-list.css',
})
export class TransactionList {
    private readonly apiService = inject(APIService);

    transactions$ = this.apiService.getAll<Transaction[]>("transactions");
}
