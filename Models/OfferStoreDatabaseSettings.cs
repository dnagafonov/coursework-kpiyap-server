using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coursework_kpiyap.Models
{
    public class OfferStoreDatabaseSettings : IOfferStoreDatabaseSettings
    {
        public string OffersCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IOfferStoreDatabaseSettings
    {
        string OffersCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
