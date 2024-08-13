using AutoMapper;
using PhotosiAddressBooks.Dto;
using PhotosiAddressBooks.Repository.AddressBook;

namespace PhotosiAddressBooks.Service;

public class AddressBookService : IAddressBookService
{
    private readonly IAddressBookRepository _addressBookRepository;
    private readonly IMapper _mapper;

    public AddressBookService(IAddressBookRepository addressBookRepository, IMapper mapper)
    {
        _addressBookRepository = addressBookRepository;
        _mapper = mapper;
    }

    public async Task<List<AddressBookDto>> GetAsync()
    {
        var addressBooks = await _addressBookRepository.GetAsync();
        return _mapper.Map<List<AddressBookDto>>(addressBooks);
    }
}