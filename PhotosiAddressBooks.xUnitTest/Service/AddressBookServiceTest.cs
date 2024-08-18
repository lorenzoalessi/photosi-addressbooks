using AutoMapper;
using Moq;
using PhotosiAddressBooks.Dto;
using PhotosiAddressBooks.Exceptions;
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
    public async Task GetByIdAsync_ShouldReturnNull_IfNotAddressFound()
    {
        // Arrange
        var service = GetService();
        var id = _faker.Int(1);
        
        // Act
        var result = await service.GetByIdAsync(id);

        // Assert
        _mockAddressBookRepository.Verify(x => x.GetByIdAsync(id), Times.Once);
        Assert.Null(result);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnObject_IfAddressFound()
    {
        // Arrange
        var service = GetService();
        var generatedAddressBook = GenerateAddressBook();
        _mockAddressBookRepository.Setup(x => x.GetByIdAsync(generatedAddressBook.Id))
            .ReturnsAsync(generatedAddressBook);

        // Act
        var result = await service.GetByIdAsync(generatedAddressBook.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(result.Id, generatedAddressBook.Id);
        Assert.Equal(result.CountryName, generatedAddressBook.CountryName);
        Assert.Equal(result.AddressName, generatedAddressBook.AddressName);
        Assert.Equal(result.AddressNumber, generatedAddressBook.AddressNumber);
        Assert.Equal(result.Cap, generatedAddressBook.Cap);
        Assert.Equal(result.CityName, generatedAddressBook.CityName);
        _mockAddressBookRepository.Verify(x => x.GetByIdAsync(generatedAddressBook.Id), Times.Once);
    }

    [Fact]
    public void UpdateAsync_ShouldThrowException_IfNoAddressBookFound()
    {
        // Arrange
        var service = GetService();
        var id = _faker.Int(1);
        
        // Act
        Assert.ThrowsAsync<AddressBookException>(async () => await service.UpdateAsync(id, new AddressBookDto()));

        // Assert
        _mockAddressBookRepository.Verify(x => x.GetByIdAsync(id), Times.Once);
        _mockAddressBookRepository.Verify(x => x.SaveAsync(), Times.Never);
    }
    
    [Fact]
    public async Task UpdateAsync_ShouldUpdateAddressBook_IfAddressBookFound()
    {
        // Arrange
        var service = GetService();

        var id = _faker.Int(1);
        var input = GenerateAddressBookDto();
        var addressBook = GenerateAddressBook();
        
        _mockAddressBookRepository.Setup(x => x.GetByIdAsync(id))
            .ReturnsAsync(addressBook);
        
        // Act
        var result = await service.UpdateAsync(id, input);

        // Assert
        Assert.Equal(result.Id, input.Id);
        Assert.Equal(result.AddressName, input.AddressName);
        Assert.Equal(result.AddressNumber, input.AddressNumber);
        Assert.Equal(result.Cap, input.Cap);
        Assert.Equal(result.CountryName, input.CountryName);
        Assert.Equal(result.CityName, input.CityName);
        
        _mockAddressBookRepository.Verify(x => x.GetByIdAsync(id), Times.Once);
        _mockAddressBookRepository.Verify(x => x.SaveAsync(), Times.Once);
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

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task DeleteAsync_ShouldReturnTrueOrFalse_IfAddressBookFoundedOrNot(bool founded)
    {
        // Arrange
        var service = GetService();
        var input = _faker.Int();

        _mockAddressBookRepository.Setup(x => x.DeleteAsync(input))
            .ReturnsAsync(founded);
        
        // Act
        var result = await service.DeleteAsync(input);

        // Assert
        _mockAddressBookRepository.Verify(x => x.DeleteAsync(input), Times.Once);
        Assert.Equal(result, founded);
    }

    [Fact]
    public async Task AddAsync_ShouldAddObject_Always()
    {
        // Arrange
        var service = GetService();
        var input = GenerateAddressBookDto();
        
        // Act
        var result = await service.AddAsync(input);

        // Assert
        _mockAddressBookRepository.Verify(x => x.AddAsync(It.IsAny<AddressBook>()), Times.Once);
        Assert.Multiple(() =>
        {
            Assert.Equal(result.Id, input.Id);
            Assert.Equal(result.AddressName, input.AddressName);
            Assert.Equal(result.AddressNumber, input.AddressNumber);
            Assert.Equal(result.CountryName, input.CountryName);
            Assert.Equal(result.Cap, input.Cap);
            Assert.Equal(result.CityName, input.CityName); 
        });
    }
    
    private IAddressBookService GetService() => new AddressBookService(_mockAddressBookRepository.Object, _mapper);

    private AddressBookDto GenerateAddressBookDto()
    {
        return new AddressBookDto
        {
            Id = _faker.Int(1),
            AddressName = _faker.String2(1, 100),
            AddressNumber = _faker.String2(1, 10),
            Cap = _faker.String2(1, 100),
            CityName = _faker.String2(1, 100),
            CountryName = _faker.String2(1, 100)
        };
    }
    
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