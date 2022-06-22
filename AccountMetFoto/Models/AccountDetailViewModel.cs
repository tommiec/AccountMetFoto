using AccountMetFoto.Domain;
using System.ComponentModel;

namespace AccountMetFoto.Models
{
    public class AccountDetailViewModel
    {
        [DisplayName("Voornaam")]
        public string FirstName { get; set; }

        [DisplayName("Familienaam")]
        public string LastName { get; set; }

        [DisplayName("Geboortedatum")]
        public DateOnly DOB { get; set; }

        [DisplayName("Geslacht")]
        public Gender Gender { get; set; }

        [DisplayName("Adres")]
        public string Address { get; set; }

        [DisplayName("Foto")]
        public string PhotoUrl { get; set; }
    }
}
