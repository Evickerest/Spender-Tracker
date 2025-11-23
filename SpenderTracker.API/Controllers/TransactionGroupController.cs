using Microsoft.AspNetCore.Http;
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
    public IActionResult GetById(int id)
    {
        TransactionGroupDto? dto = _groupService.GetById(id);
        if (dto == null)
        {
            return NotFound($"Could not find Transaction Group with specified id {id}.");
        }

        return Ok(dto);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_groupService.GetAll());
    }

    [HttpPost]
    public IActionResult Insert([FromBody] TransactionGroupDto dto)
    {
        if (dto == null)
        {
            return BadRequest("Transaction Group must be included in the body");
        }

        TransactionGroupDto? group = _groupService.Insert(dto);
        if (group == null)
        {
            return StatusCode(500, "An error occurred while creating the TransactionGroup.");
        }

        return CreatedAtAction(nameof(GetById), new { id = group.Id }, group);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] TransactionGroupDto dto)
    {
        if (dto == null)
        {
            return BadRequest("Transaction Group must be included in the body");
        }

        if (dto.Id != id)
        {
            return BadRequest("Transaction Group id does not match specified id.");
        }

        if (!_groupService.DoesExist(id))
        {
            return NotFound($"Could not find Transaction Group with specified id {id}.");
        }

        bool success = _groupService.Update(dto);
        if (!success)
        {
            return StatusCode(500, "An error occurred while updating the Transaction Group.");
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        TransactionGroupDto? dto = _groupService.GetById(id);
        if (dto == null)
        {
            return NotFound($"Could not find Transaction Group with specified id {id}.");
        }

        bool success = _groupService.Delete(dto);
        if (!success)
        {
            return StatusCode(500, "An error occurred while deleting the Transaction Group.");
        }

        return NoContent();
    }
}
