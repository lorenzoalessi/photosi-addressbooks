using Bogus;
using Microsoft.EntityFrameworkCore;
using PhotosiAddressBooks.Model;

namespace PhotosiAddressBooks.xUnitTest;

public abstract class TestSetup
{
    protected readonly Context _context;

    protected readonly Randomizer _faker;

    protected TestSetup()
    {
        _faker = new Randomizer();

        var dbOptions = new DbContextOptionsBuilder<Context>()
            .EnableSensitiveDataLogging()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new Context(dbOptions);
    }
}