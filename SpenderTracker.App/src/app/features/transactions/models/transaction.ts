export interface Transaction {
    id: number,
    transactionTypeId: number,
    transactionGroupId: number,
    transactionMethodId: number,
    amount: number,
    description: string | null,
    timestamp: string
}
