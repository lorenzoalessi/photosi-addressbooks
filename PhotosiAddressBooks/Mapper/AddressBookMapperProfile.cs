using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using PhotosiAddressBooks.Dto;
using PhotosiAddressBooks.Model;

namespace PhotosiAddressBooks.Mapper;

[ExcludeFromCodeCoverage]
public class AddressBookMapperProfile : Profile
{
    public AddressBookMapperProfile()
    {
        CreateMap<AddressBook, AddressBookDto>()
            .ReverseMap();
    }
}