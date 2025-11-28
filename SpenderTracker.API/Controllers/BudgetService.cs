using Microsoft.AspNetCore.Mvc;
using SpenderTracker.Core.Interfaces;
using SpenderTracker.Data.Dto;

namespace SpenderTracker.API.Controllers;

[Route("api/budgets")]
[ApiController]
public class BudgetController : ControllerBase
{
    private readonly IBudgetService _budgetService;
    private readonly ITransactionGroupService _groupService;

    public BudgetController(
        IBudgetService budgetService,
        ITransactionGroupService groupService)
    {
        _budgetService = budgetService;
        _groupService = groupService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        BudgetDto? dto = await _budgetService.GetById(id, ct);
        if (dto == null)
        {
            return NotFound($"Could not find Budget with specified id {id}.");
        }

        return Ok(dto); 
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var dtos = await _budgetService.GetAll(ct); 
        return Ok(dtos); 
    }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] BudgetDto dto, CancellationToken ct)
    { 
        if (dto == null)
        {
            return BadRequest("Budget must be included in the body");
        }
        
        if (dto.TransactionGroupId.HasValue)
        {
            bool groupExists = await _groupService.DoesExist(dto.TransactionGroupId.Value, ct);
            if (!groupExists)
            {
                return BadRequest($"Could not find Transaction Group with specified id {dto.TransactionGroupId}.");
            }
        } 

        BudgetDto? budget = await _budgetService.Insert(dto); 
        if (budget == null)
        {
            return StatusCode(500, "An error occurred while creating the Budget.");
        } 

        return CreatedAtAction(nameof(GetById), new { id = budget.Id }, budget); 
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] BudgetDto dto, CancellationToken ct)
    {
        if (dto == null)
        {
            return BadRequest("Budget must be included in the body");
        }

        if (dto.Id != id)
        {
            return BadRequest("Budget id does not match specified id.");
        } 

        if (!await _budgetService.DoesExist(id, ct))
        {
            return NotFound($"Could not find Budget with specified id {id}.");
        } 

        bool success = await _budgetService.Update(dto); 
        if (!success)
        {
            return StatusCode(500, "An error occurred while updating the Budget.");
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        if (!await _budgetService.DoesExist(id, ct))
        {
            return NotFound($"Could not find Budget with specified id {id}.");
        } 

        bool success = await _budgetService.Delete(id); 
        if (!success)
        {
            return StatusCode(500, "An error occurred while deleting the Budget.");
        }

        return NoContent();
    }
}
