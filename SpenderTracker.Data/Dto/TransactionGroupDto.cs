using SpenderTracker.Data.Interface;

namespace SpenderTracker.Data.Dto;

public class TransactionGroupDto : IDto
{
    public int Id { get; set; }
    public string GroupName { get; set; } = null!;
}
