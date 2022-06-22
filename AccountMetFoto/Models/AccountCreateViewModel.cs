using AccountMetFoto.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AccountMetFoto.Models
{
    public class AccountCreateViewModel
    {
        [DisplayName("Voornaam")]
        [Required, MaxLength(20)]
        public string FirstName { get; set; }

        [DisplayName("Familienaam")]
        [Required, MaxLength(20)]
        public string LastName { get; set; }

        [DisplayName("Geboortedatum")]
        [Required]
        public DateOnly DOB { get; set; }

        [DisplayName("Geslacht")]
        [Required]
        public Gender Gender { get; set; }

        [DisplayName("Adres")]
        [Required, MaxLength(75)]
        public string Address { get; set; }

        [DisplayName("Foto")]
        public IFormFile Photo { get; set; }
    }
}
