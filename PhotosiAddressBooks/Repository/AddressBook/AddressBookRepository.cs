using PhotosiAddressBooks.Model;

namespace PhotosiAddressBooks.Repository.AddressBook;

public class AddressBookRepository : GenericRepository<Model.AddressBook>, IAddressBookRepository
{
    public AddressBookRepository(Context context) : base(context)
    {
    }
}