using AccountMetFoto.Domain;

namespace AccountMetFoto.Database
{
    public interface IAccountDatabase
    {
        Account Insert(Account account);
        IEnumerable<Account> GetAccounts();
        Account GetAccount(int Id);
        void Update(int Id, Account account);
        void Delete(int Id);
    }
}
