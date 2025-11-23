using SpenderTracker.Core.Interfaces;
using SpenderTracker.Data.Context;
using SpenderTracker.Data.Dto;
using SpenderTracker.Data.Model;

namespace SpenderTracker.Core.Services;

public class BudgetService : BaseService<Budget, BudgetDto>, IBudgetService
{
    public BudgetService(ApplicationContext dbContext) : base(dbContext)
    { 
    }
}
