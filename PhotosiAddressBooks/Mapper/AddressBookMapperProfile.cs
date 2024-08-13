using AutoMapper;
using PhotosiAddressBooks.Dto;
using PhotosiAddressBooks.Model;

namespace PhotosiAddressBooks.Mapper;

public class AddressBookMapperProfile : Profile
{
    public AddressBookMapperProfile()
    {
        CreateMap<AddressBook, AddressBookDto>()
            .ReverseMap();
    }
}