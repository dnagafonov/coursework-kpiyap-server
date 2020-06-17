using coursework_kpiyap.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace coursework_kpiyap.Services
{
    public class OfferService
    {
        private readonly IMongoCollection<Offer> _offers;

        public OfferService(IOfferStoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _offers = database.GetCollection<Offer>(settings.OffersCollectionName);
        }
        //CREATE OFFER METHOD
        public JsonResult Create(Offer offer)
        {
            try
            {
                _offers.InsertOne(offer);
                return new JsonResult(new { status = 201 });
            }
            catch
            {
                return new JsonResult(new { status = 400 });
            }
        }
    }
}
