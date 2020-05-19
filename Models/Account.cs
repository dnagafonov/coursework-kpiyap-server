﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace coursework_kpiyap.Models
{
    public class Account
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string username { get; set; }

        public string password { get; set; }

        public string email { get; set; }

        public string currency { get; set; }

        public List<Service> cart { get; set; }
    }
}
