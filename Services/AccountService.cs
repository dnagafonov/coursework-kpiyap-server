using coursework_kpiyap.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace coursework_kpiyap.Services
{
    public class AccountService
    {
        private readonly IMongoCollection<Account> _accounts;

        public AccountService(IAccountStoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _accounts = database.GetCollection<Account>(settings.AccountsCollectionName);
        }

        public List<Account> Get() =>
            _accounts.Find(account => true).ToList();

        public Account Find(string username, string password) =>
            _accounts.Find(account => account.username.Equals(username) && account.password.Equals(password)).FirstOrDefault();

        public Account Get(string id) =>
            _accounts.Find<Account>(account => account.Id == id).FirstOrDefault();

        public Account Create(Account account)
        {
            _accounts.InsertOne(account);
            return account;
        }

        public void Update(string id, Account accountIn) =>
            _accounts.ReplaceOne(account => account.Id == id, accountIn);

        public void Remove(Account accountIn) =>
            _accounts.DeleteOne(account => account.Id == accountIn.Id);

        public void Remove(string id) =>
            _accounts.DeleteOne(account => account.Id == id);

    }
}
