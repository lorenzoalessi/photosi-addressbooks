using AutoMapper;
using PhotosiAddressBooks.Dto;
using PhotosiAddressBooks.Exceptions;
using PhotosiAddressBooks.Model;
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

    public async Task<AddressBookDto> GetByIdAsync(int id)
    {
        var addressBook = await _addressBookRepository.GetByIdAsync(id);
        return _mapper.Map<AddressBookDto>(addressBook);
    }

    public async Task<AddressBookDto> UpdateAsync(int id, AddressBookDto addressBookDto)
    {
        var addressBook = await _addressBookRepository.GetByIdAsync(id);
        if (addressBook == null)
            throw new AddressBookException($"L'indirizzo con ID {id} non esiste");

        addressBook.AddressName = addressBookDto.AddressName;
        addressBook.AddressNumber = addressBookDto.AddressNumber;
        addressBook.Cap = addressBookDto.Cap;
        addressBook.CityName = addressBookDto.CityName;
        addressBook.CountryName = addressBookDto.CountryName;

        await _addressBookRepository.SaveAsync();

        return addressBookDto;
    }

    public async Task<AddressBookDto> AddAsync(AddressBookDto addressBookDto)
    {
        var addressBook = _mapper.Map<AddressBook>(addressBookDto);
        await _addressBookRepository.AddAsync(addressBook);

        // Aggiorno l'Id della dto senza rimappare
        addressBookDto.Id = addressBook.Id;
        return addressBookDto;
    }

    public async Task<bool> DeleteAsync(int id) => await _addressBookRepository.DeleteAsync(id);
}