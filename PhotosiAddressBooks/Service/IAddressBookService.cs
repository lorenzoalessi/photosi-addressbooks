using PhotosiAddressBooks.Dto;

namespace PhotosiAddressBooks.Service;

public interface IAddressBookService
{
    Task<List<AddressBookDto>> GetAsync();
    
    Task<AddressBookDto> GetByIdAsync(int id);

    Task<AddressBookDto> UpdateAsync(int id, AddressBookDto addressBookDto);
    
    Task<AddressBookDto> AddAsync(AddressBookDto addressBookDto);
    
    Task<bool> DeleteAsync(int id);
}