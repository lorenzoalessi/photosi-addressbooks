using System.Diagnostics.CodeAnalysis;

namespace PhotosiAddressBooks.Exceptions;

[ExcludeFromCodeCoverage]
public class AddressBookException : Exception
{
    public AddressBookException()
    {
    }

    public AddressBookException(string message) : base(message)
    {
    }

    public AddressBookException(Exception exception)
    {
    }
}