using AutoMapper;
using Moq;
using PhotosiAddressBooks.Mapper;
using PhotosiAddressBooks.Model;
using PhotosiAddressBooks.Repository.AddressBook;
using PhotosiAddressBooks.Service;

namespace PhotosiAddressBooks.xUnitTest.Service;

public class AddressBookServiceTest : TestSetup
{
    private readonly Mock<IAddressBookRepository> _mockAddressBookRepository;
    private readonly IMapper _mapper;

    public AddressBookServiceTest()
    {
        _mockAddressBookRepository = new Mock<IAddressBookRepository>();

        var config = new MapperConfiguration(conf =>
        {
            conf.AddProfile(typeof(AddressBookMapperProfile));
        });

        _mapper = config.CreateMapper();
    }

    [Fact]
    public async Task GetAsync_ShouldReturnList_Always()
    {
        // Arrange
        var service = GetService();

        var addressBooks = Enumerable.Range(0, _faker.Int(10, 30))
            .Select(_ => GenerateAddressBook())
            .ToList();
        
        // Setup mock del repository
        _mockAddressBookRepository.Setup(x => x.GetAsync())
            .ReturnsAsync(addressBooks);
        
        // Act
        var result = await service.GetAsync();
        
        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.Equal(result.Count, addressBooks.Count);
            Assert.Empty(result.Select(x => x.Id).Except(addressBooks.Select(x => x.Id)));
            Assert.Empty(result.Select(x => x.AddressName).Except(addressBooks.Select(x => x.AddressName)));
            Assert.Empty(result.Select(x => x.AddressNumber).Except(addressBooks.Select(x => x.AddressNumber)));
            Assert.Empty(result.Select(x => x.Cap).Except(addressBooks.Select(x => x.Cap)));
            Assert.Empty(result.Select(x => x.CityName).Except(addressBooks.Select(x => x.CityName)));
            Assert.Empty(result.Select(x => x.CountryName).Except(addressBooks.Select(x => x.CountryName)));
            
            // Campi obbligatori
            Assert.All(result, x => Assert.False(string.IsNullOrEmpty(x.AddressName)));
            Assert.All(result, x => Assert.False(string.IsNullOrEmpty(x.AddressNumber)));
            Assert.All(result, x => Assert.False(string.IsNullOrEmpty(x.Cap)));
            Assert.All(result, x => Assert.False(string.IsNullOrEmpty(x.CityName)));
            Assert.All(result, x => Assert.False(string.IsNullOrEmpty(x.CountryName)));
        });
        
        _mockAddressBookRepository.Verify(x => x.GetAsync(), Times.Once);
    }
    
    private IAddressBookService GetService() => new AddressBookService(_mockAddressBookRepository.Object, _mapper);
    
    private AddressBook GenerateAddressBook()
    {
        return new AddressBook()
        {
            Id = _faker.Int(1),
            AddressName = _faker.String2(1, 100),
            AddressNumber = _faker.String2(1, 10),
            Cap = _faker.String2(1, 100),
            CityName = _faker.String2(1, 100),
            CountryName = _faker.String2(1, 100)
        };
    }
}