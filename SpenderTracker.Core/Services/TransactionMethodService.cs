using Microsoft.EntityFrameworkCore;
using SpenderTracker.Core.Interfaces;
using SpenderTracker.Data.Context;
using SpenderTracker.Data.Dto;
using SpenderTracker.Data.Model;

namespace SpenderTracker.Core.Services;

public class TransactionMethodService : BaseService<TransactionMethod, TransactionMethodDto>, ITransactionMethodService
{ 
    public TransactionMethodService(ApplicationContext dbContext) : base (dbContext)
    { 
    }

    public List<TransactionMethodDto> GetAllByAccountId(int accountId)
    {
        return _dbContext.TransactionMethods.AsNoTracking().
            Where(t => t.AccountId == accountId).
            Select(t => t.ToDto()).
            ToList();
    }
}
