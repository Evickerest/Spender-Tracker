using SpenderTracker.Core.Interfaces;
using SpenderTracker.Data.Context;
using SpenderTracker.Data.Dto;
using SpenderTracker.Data.Model;

namespace SpenderTracker.Core.Services;

public class TransactionGroupService : BaseService<TransactionGroup, TransactionGroupDto>, ITransactionGroupService
{
    public TransactionGroupService(ApplicationContext dbContext) : base (dbContext)
    { 
    }
}
