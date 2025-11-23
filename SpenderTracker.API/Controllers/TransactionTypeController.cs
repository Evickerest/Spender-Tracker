using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpenderTracker.Core.Interfaces;
using SpenderTracker.Data.Dto;

namespace SpenderTracker.API.Controllers;

[Route("api/transaction-types")]
[ApiController]
public class TransactionTypeController : ControllerBase
{
    private readonly ITransactionTypeService _typeService;

    public TransactionTypeController(ITransactionTypeService transactionService)
    {
        _typeService = transactionService;
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        TransactionTypeDto? dto = _typeService.GetById(id);
        if (dto == null)
        {
            return NotFound($"Could not find Transaction Type with specified id {id}.");
        }

        return Ok(dto);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_typeService.GetAll());
    }

    [HttpPost]
    public IActionResult Insert([FromBody] TransactionTypeDto dto)
    {
        if (dto == null)
        {
            return BadRequest("Transaction Type must be included in the body");
        }

        TransactionTypeDto? type = _typeService.Insert(dto);
        if (type == null)
        {
            return StatusCode(500, "An error occurred while creating the TransactionType.");
        }

        return CreatedAtAction(nameof(GetById), new { id = type.Id }, type);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] TransactionTypeDto dto)
    {
        if (dto == null)
        {
            return BadRequest("Transaction Type must be included in the body");
        }

        if (dto.Id != id)
        {
            return BadRequest("Transaction Type id does not match specified id.");
        }

        if (!_typeService.DoesExist(id))
        {
            return NotFound($"Could not find Transaction Type with specified id {id}.");
        }

        bool success = _typeService.Update(dto);
        if (!success)
        {
            return StatusCode(500, "An error occurred while updating the Transaction Type.");
        }

        return NoContent(); 
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        TransactionTypeDto? dto = _typeService.GetById(id);
        if (dto == null)
        {
            return NotFound($"Could not find Transaction Type with specified id {id}.");
        }

        bool success = _typeService.Delete(dto);
        if (!success)
        {
            return StatusCode(500, "An error occurred while deleting the Transaction Type.");
        }

        return NoContent();
    }
}
