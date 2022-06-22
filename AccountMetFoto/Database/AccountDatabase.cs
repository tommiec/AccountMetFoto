using AccountMetFoto.Domain;

namespace AccountMetFoto.Database
{
    public class AccountDatabase : IAccountDatabase
    {
        private int counter;
        private readonly List<Account> accounts;

        public AccountDatabase()
        {
            accounts = new List<Account>();

            Insert(new Account
            {
                FirstName = "Thomas",
                LastName="Coppens",
                DOB=new DateOnly(1983,04,21),
                Gender = Gender.M,
                Address="Schildersstraat 2, 9040 Gent"
            });

            Insert(new Account
            {
                FirstName = "Lieselot",
                LastName = "Rijckaert",
                DOB = new DateOnly(1985, 06, 25),
                Gender = Gender.F,
                Address = "Schildersstraat 2, 9040 Gent"
            });

            Insert(new Account
            {
                FirstName = "Clara",
                LastName = "Coppens",
                DOB = new DateOnly(2019, 02, 23),
                Gender = Gender.F,
                Address = "Schildersstraat 2, 9040 Gent"
            });

        }

        public void Delete(int Id)
        {
            var account = accounts.FirstOrDefault(x => x.Id == Id);
            if (account != null)
            {
                accounts.Remove(account);
            }
        }

        public Account GetAccount(int Id)
        {
            return accounts.FirstOrDefault(x => x.Id == Id);
        }

        public IEnumerable<Account> GetAccounts()
        {
            return accounts;
        }

        public Account Insert(Account account)
        {
            account.Id = ++counter;
            accounts.Add(account);
            return account;
        }

        public void Update(int Id, Account updatedAccount)
        {
            var account = accounts.FirstOrDefault(x => x.Id == Id);
            if (account != null)
            {
                account.FirstName = updatedAccount.FirstName;
                account.LastName = updatedAccount.LastName;
                account.DOB = updatedAccount.DOB;
                account.Gender = updatedAccount.Gender;
                account.Address = updatedAccount.Address;
                account.PhotoUrl = updatedAccount.PhotoUrl;
            }
        }
    }
}
