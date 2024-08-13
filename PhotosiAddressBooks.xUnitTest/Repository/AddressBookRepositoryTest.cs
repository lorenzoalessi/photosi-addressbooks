using PhotosiAddressBooks.Model;
using PhotosiAddressBooks.Repository.AddressBook;

namespace PhotosiAddressBooks.xUnitTest.Repository;

public class AddressBookRepositoryTest : TestSetup
{
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
    
    private IAddressBookRepository GetRepository() => new AddressBookRepository(_context);

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