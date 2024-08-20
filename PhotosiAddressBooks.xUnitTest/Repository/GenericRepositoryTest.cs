using PhotosiAddressBooks.Model;
using PhotosiAddressBooks.Repository;
using PhotosiAddressBooks.Repository.AddressBook;

namespace PhotosiAddressBooks.xUnitTest.Repository;

public class GenericRepositoryTest : TestSetup
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task GetByIdAsync_ShouldReturnEntity_IfFounded(bool founded)
    {
        // Arrange
        var repository = GetRepository();
        var id = founded ? GenerateAddressBookAndSave().Id : 0;

        // Act
        var result = await repository.GetByIdAsync(id);

        // Assert
        if (founded) Assert.Equal(result?.Id, id);
        else Assert.Null(result);
    }
    
    [Fact]
    public async Task GetAsync_ShouldReturnList_Always()
    {
        // Arrange
        var repository = GetRepository();

        var addressBooks = Enumerable.Range(0, _faker.Int(10, 30))
            .Select(_ => GenerateAddressBookAndSave())
            .ToList();
        
        // Act
        var result = await repository.GetAsync();
        
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
    }

    [Fact]
    public async Task AddAsync_ShouldAddObject_Always()
    {
        // Arrange
        var repository = GetRepository();
        var input = new AddressBook()
        {
            Id = _faker.Int(1),
            AddressName = _faker.String2(1, 100),
            AddressNumber = _faker.String2(1, 10),
            Cap = _faker.String2(1, 100),
            CityName = _faker.String2(1, 100),
            CountryName = _faker.String2(1, 100)
        };
        
        // Act
        var result = await repository.AddAsync(input);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Id > 0);
        Assert.Equal(input.CountryName, result.CountryName);
        Assert.Equal(input.AddressName, result.AddressName);
        Assert.Equal(input.AddressNumber, result.AddressNumber);
        Assert.Equal(input.CityName, result.CityName);
        Assert.Equal(input.Cap, result.Cap);
        Assert.Equal(input.Id, result.Id);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnFalse_IfObjectNotFound()
    {
        // Arrange
        var repository = GetRepository();
        
        // Act
        var result = await repository.DeleteAsync(_faker.Int(1));
        
        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue_IfObjectFound()
    {
        // Arrange
        var repository = GetRepository();
        var addressBook = GenerateAddressBookAndSave();
        
        // Act
        var result = await repository.DeleteAsync(addressBook.Id);
        
        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task SaveAsync_ShouldNotThrowException_Always()
    {
        // Arrange
        var repository = GetRepository();
        
        var input = new AddressBook()
        {
            Id = _faker.Int(1),
            AddressName = _faker.String2(1, 100),
            AddressNumber = _faker.String2(1, 10),
            Cap = _faker.String2(1, 100),
            CityName = _faker.String2(1, 100),
            CountryName = _faker.String2(1, 100)
        };
        await _context.AddAsync(input);

        // Act
        await repository.SaveAsync();
        
        // Assert
        Assert.True(input.Id > 0);
    }
    
    private IGenericRepository<AddressBook> GetRepository() => new GenericRepository<AddressBook>(_context);

    private AddressBook GenerateAddressBookAndSave()
    {
        var addressBook = new AddressBook()
        {
            Id = _faker.Int(1),
            AddressName = _faker.String2(1, 100),
            AddressNumber = _faker.String2(1, 10),
            Cap = _faker.String2(1, 100),
            CityName = _faker.String2(1, 100),
            CountryName = _faker.String2(1, 100)
        };

        _context.Add(addressBook);
        _context.SaveChanges();

        return addressBook;
    }
}