using coursework_kpiyap.Models;
using MongoDB.Bson;
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

        public Account AddToCart(string id, Service service)
        {
            var filter = Builders<Account>.Filter.Where(x => x.Id == id);
            var update = Builders<Account>.Update.Push("cart", service);
            _accounts.UpdateOne(filter, update).ToJson();
            return _accounts.Find(account => account.Id == id).FirstOrDefault();

        }

        public Account DeleteFromCart(string id, Service service)
        {
            var filter = Builders<Account>.Filter.Where(x => x.Id == id);
            //ability to add more fields to filter
            var update = Builders<Account>.Update.PullFilter("cart", Builders<Service>.Filter.Eq(e => e.Id, service.Id));
            _accounts.UpdateOne(filter, update);
            return _accounts.Find(account => account.Id == id).FirstOrDefault();
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
