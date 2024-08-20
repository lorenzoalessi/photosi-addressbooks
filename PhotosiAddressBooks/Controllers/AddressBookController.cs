using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using PhotosiAddressBooks.Dto;
using PhotosiAddressBooks.Exceptions;
using PhotosiAddressBooks.Service;

namespace PhotosiAddressBooks.Controllers;

// I metodi dei controller vengono esclusi perche si potrebbero testare tramite integration test al posto di unit test
[ExcludeFromCodeCoverage]
[Route("api/v1/address-books")]
[ApiController]
public class AddressBookController : ControllerBase
{
    private readonly IAddressBookService _addressBookService;

    public AddressBookController(IAddressBookService addressBookService)
    {
        _addressBookService = addressBookService;
    }

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _addressBookService.GetAsync());
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        if (id < 1)
            return BadRequest("ID fornito non valido");

        return Ok(await _addressBookService.GetByIdAsync(id));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] AddressBookDto addressBookDto)
    {
        if (id < 1)
            return BadRequest("ID fornito non valido");

        try
        {
            var result = await _addressBookService.UpdateAsync(id, addressBookDto);
            return Ok($"Indirizzo con ID {result.Id} salvato successo");
        }
        catch (AddressBookException e)
        {
            return BadRequest($"Errore nella richiesta di aggiornamento: {e.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddressBookDto addressBookDto) => 
        Ok(await _addressBookService.AddAsync(addressBookDto));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id < 1)
            return BadRequest("ID fornito non valido");
        
        var deleted = await _addressBookService.DeleteAsync(id);
        if (!deleted)
            return StatusCode(500, "Errore nella richiesta di eliminazione");
            
        return Ok($"Indirizzo con ID {id} eliminato con successo");
    }
}