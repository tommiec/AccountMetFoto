using AccountMetFoto.Domain;

namespace AccountMetFoto.Models
{
    public class AccountListViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
    }
}
