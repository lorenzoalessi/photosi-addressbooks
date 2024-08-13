using Microsoft.AspNetCore.Mvc;
using PhotosiAddressBooks.Service;

namespace PhotosiAddressBooks.Controllers;

[Route("api/v1/address-book")]
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
}