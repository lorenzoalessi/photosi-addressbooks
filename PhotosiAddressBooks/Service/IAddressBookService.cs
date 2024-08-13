using PhotosiAddressBooks.Dto;

namespace PhotosiAddressBooks.Service;

public interface IAddressBookService
{
    Task<List<AddressBookDto>> GetAsync();
}