using SpenderTracker.Data.Interface;

namespace SpenderTracker.Data.Dto;

public class TransactionTypeDto : IDto
{
    public int Id { get; set; }
    public string TypeName { get; set; } = null!; 
}
