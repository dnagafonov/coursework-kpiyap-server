using coursework_kpiyap.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
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

        public JsonResult AddToCart(string id, CartServiceRequest service)
        {
            try
            {
                var filter = Builders<Account>.Filter.Where(x => x._id == id);
                var res = new CartServiceResponse();
                res._id = ObjectId.GenerateNewId().ToString();
                res.currency = service.currency;
                res.name = service.name;
                res.price = service.price;
                res.serviceId = service._id;
                res.currentPrice = service.currentPrice;
                res.description = service.description;
                res.type = service.type;
                var update = Builders<Account>.Update.Push("cart", res);
                _accounts.UpdateOne(filter, update);
                return new JsonResult(new { status = 201, cart = _accounts.Find(account => account._id == id).FirstOrDefault().cart });
            }
            catch(Exception e)
            {
                return new JsonResult(new { status = 400, error = e.Message });
            }
        }

        public JsonResult DeleteFromCart(string id, CartServiceResponse service)
        {
            try
            {
                var filter = Builders<Account>.Filter.Where(x => x._id == id);
                //ability to add more fields to filter
                var update = Builders<Account>.Update.PullFilter("cart", Builders<CartServiceResponse>.Filter.Eq(e => e._id, service._id));
                _accounts.UpdateOne(filter, update);
                return new JsonResult(new { status = 200, cart = _accounts.Find(account => account._id == id).FirstOrDefault().cart });
            }
            catch (Exception e)
            {
                return new JsonResult(new { status = 400, error = e.Message });
            }
        }

        public List<Account> Get() =>
            _accounts.Find(account => true).ToList();

        public Account Find(string username, string password) =>
            _accounts.Find(account => account.username.Equals(username) && account.password.Equals(password)).FirstOrDefault();

        public Account Get(string id) =>
            _accounts.Find<Account>(account => account._id == id).FirstOrDefault();

        public JsonResult Create(Account account)
        {
            try
            {
                _accounts.InsertOne(account);
                return new JsonResult(new { status= 201, account });
            }
            catch
            {
                return new JsonResult(new { status= 400 });
            }
        }

        public void Update(string id, Account accountIn) =>
            _accounts.ReplaceOne(account => account._id == id, accountIn);

        public void Remove(Account accountIn) =>
            _accounts.DeleteOne(account => account._id == accountIn._id);

        public void Remove(string id) =>
            _accounts.DeleteOne(account => account._id == id);

    }
}
