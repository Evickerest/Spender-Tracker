using SpenderTracker.Data.Dto;
using SpenderTracker.Data.Model;

namespace SpenderTracker.Core.Interfaces;

public interface ITransactionGroupService : IBaseService<TransactionGroup, TransactionGroupDto>
{
    Task<bool> IsInTransactions(int id, CancellationToken ct);
}
