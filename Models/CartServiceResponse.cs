﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace coursework_kpiyap.Models
{
    public class CartServiceResponse
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string serviceId { get; set; }

        public string type { get; set; }

        public string name { get; set; }

        public double price { get; set; }

        public string description { get; set; }

        public CurrentPrice currentPrice { get; set; }

    }
}
