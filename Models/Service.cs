﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace coursework_kpiyap.Models
{
    public class Service
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public string type { get; set; }

        public string name { get; set; }

        public double price { get; set; }

        public string description { get; set; }
    }
}
