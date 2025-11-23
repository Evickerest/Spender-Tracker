using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpenderTracker.Core.Interfaces;
using SpenderTracker.Data.Dto;

namespace SpenderTracker.API.Controllers;

[Route("api/transaction")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        TransactionDto? dto = _transactionService.GetById(id);
        if (dto == null)
        {
            return NotFound($"Could not find Transaction with specified id {id}.");
        }

        return Ok(dto);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_transactionService.GetAll());
    }

    [HttpPost]
    public IActionResult Insert([FromBody] TransactionDto dto)
    {
        if (dto == null)
        {
            return BadRequest("Transaction must be included in the body");
        }

        TransactionDto? transaction = _transactionService.Insert(dto);
        if (transaction == null)
        {
            return StatusCode(500, "An error occurred while creating the Transaction.");
        }

        return CreatedAtAction(nameof(GetById), new { id = transaction.Id }, transaction);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] TransactionDto dto)
    {
        if (dto == null)
        {
            return BadRequest("Transaction must be included in the body");
        }

        if (dto.Id != id)
        {
            return BadRequest("Transaction id does not match specified id.");
        }

        if (!_transactionService.DoesExist(id))
        {
            return NotFound($"Could not find Transaction with specified id {id}.");
        }

        bool success = _transactionService.Update(dto);
        if (!success)
        {
            return StatusCode(500, "An error occurred while updating the Transaction.");
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        TransactionDto? dto = _transactionService.GetById(id);
        if (dto == null)
        {
            return NotFound($"Could not find Transaction with specified id {id}.");
        }

        bool success = _transactionService.Delete(dto);
        if (!success)
        {
            return StatusCode(500, "An error occurred while deleting the Transaction.");
        }

        return NoContent();
    }
}
