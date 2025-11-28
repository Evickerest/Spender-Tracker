using Microsoft.AspNetCore.Mvc;
using SpenderTracker.Core.Interfaces;
using SpenderTracker.Data.Dto;

namespace SpenderTracker.API.Controllers;

[Route("api/transaction-groups")]
[ApiController]
public class TransactionGroupController : ControllerBase
{
    private readonly ITransactionGroupService _groupService;

    public TransactionGroupController(ITransactionGroupService transactionService)
    {
        _groupService = transactionService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        TransactionGroupDto? dto = await _groupService.GetById(id, ct);
        if (dto == null)
        {
            return NotFound($"Could not find Transaction Group with specified id {id}.");
        }

        return Ok(dto);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var dtos = await _groupService.GetAll(ct);
        return Ok(dtos);
    }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] TransactionGroupDto dto)
    {
        if (dto == null)
        {
            return BadRequest("Transaction Group must be included in the body");
        }

        TransactionGroupDto? group = await _groupService.Insert(dto);
        if (group == null)
        {
            return StatusCode(500, "An error occurred while creating the Transaction Group.");
        }

        return CreatedAtAction(nameof(GetById), new { id = group.Id }, group);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] TransactionGroupDto dto, CancellationToken ct)
    {
        if (dto == null)
        {
            return BadRequest("Transaction Group must be included in the body");
        }

        if (dto.Id != id)
        {
            return BadRequest("Transaction Group id does not match specified id.");
        }

        if (!await _groupService.DoesExist(id, ct))
        {
            return NotFound($"Could not find Transaction Group with specified id {id}.");
        }

        bool success = await _groupService.Update(dto);
        if (!success)
        {
            return StatusCode(500, "An error occurred while updating the Transaction Group.");
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        if (!await _groupService.DoesExist(id, ct))
        {
            return NotFound($"Could not find Transaction Group with specified id {id}.");
        } 

        if (await _groupService.IsInTransactions(id, ct)) {             
            return BadRequest("Cannot delete Transaction Group as it is in at least one transaction.");
        }

        bool success = await _groupService.Delete(id);
        if (!success)
        {
            return StatusCode(500, "An error occurred while deleting the Transaction Group.");
        }

        return NoContent();
    }
}
