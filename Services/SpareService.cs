using coursework_kpiyap.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace coursework_kpiyap.Services
{
    public class SpareService
    {
        private readonly IMongoCollection<Service> _spares;

        public SpareService(ISpareStoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _spares = database.GetCollection<Service>(settings.SparesCollectionName);
        }
        //CRUD API
        public List<Service> Get() =>
            _spares.Find(spare => true).ToList();

        public Service Get(string id) =>
            _spares.Find<Service>(spare => spare._id == id).FirstOrDefault();

        public Service Create(Service spare)
        {
            _spares.InsertOne(spare);
            return spare;
        }

        public void Update(string id, Service spareIn) =>
            _spares.ReplaceOne(spare => spare._id == id, spareIn);

        public void Remove(Service spareIn) =>
            _spares.DeleteOne(spare => spare._id == spareIn._id);

        public void Remove(string id) =>
            _spares.DeleteOne(spare => spare._id == id);

    }
}
