using coursework_kpiyap.Models;
using Microsoft.AspNetCore.Mvc;
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
            var indexEmail = Builders<Account>.IndexKeys.Ascending("email");
            var indexUsername = Builders<Account>.IndexKeys.Ascending("username");
            var indexOptions = new CreateIndexOptions { Unique = true };
            var modelEmail = new CreateIndexModel<Account>(indexEmail, indexOptions);
            var modelUsername = new CreateIndexModel<Account>(indexUsername, indexOptions);

            _accounts.Indexes.CreateOne(modelEmail);
            _accounts.Indexes.CreateOne(modelUsername);
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
            var update = Builders<Account>.Update.PullFilter("cart", Builders<Service>.Filter.Eq(e => e._id, service._id));
            _accounts.UpdateOne(filter, update);
            return _accounts.Find(account => account.Id == id).FirstOrDefault();
        }

        public List<Account> Get() =>
            _accounts.Find(account => true).ToList();

        public Account Find(string username, string password) =>
            _accounts.Find(account => account.username.Equals(username) && account.password.Equals(password)).FirstOrDefault();

        public Account Get(string id) =>
            _accounts.Find<Account>(account => account.Id == id).FirstOrDefault();

        public JsonResult Create(Account account)
        {
            try
            {
                _accounts.InsertOne(account);
                return new JsonResult(new { status= 200, account });
            }
            catch
            {
                return new JsonResult(new { status= 404 });
            }
        }

        public void Update(string id, Account accountIn) =>
            _accounts.ReplaceOne(account => account.Id == id, accountIn);

        public void Remove(Account accountIn) =>
            _accounts.DeleteOne(account => account.Id == accountIn.Id);

        public void Remove(string id) =>
            _accounts.DeleteOne(account => account.Id == id);

    }
}
