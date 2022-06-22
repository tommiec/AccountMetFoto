using AccountMetFoto.Domain;

namespace AccountMetFoto.Models
{

    public class AccountViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DOB { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
    }
}
