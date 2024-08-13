using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotosiAddressBooks.Model;

[Table("address_book")]
public class AddressBook
{
    [Column("id"), Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("address_name"), Required]
    public string AddressName { get; set; }

    [Column("address_number"), Required]
    public string AddressNumber { get; set; }

    [Column("cap"), Required]
    public string Cap { get; set; }

    [Column("city_name"), Required]
    public string CityName { get; set; }
    
    [Column("country_name"), Required]
    public string CountryName { get; set; }
}