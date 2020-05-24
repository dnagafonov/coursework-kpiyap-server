using coursework_kpiyap.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace coursework_kpiyap.Services
{
    public class ServiceService
    {
        private readonly IMongoCollection<Service> _services;

        public ServiceService(IServiceStoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _services = database.GetCollection<Service>(settings.ServicesCollectionName);
        }

        public List<Service> Get() =>
            _services.Find(service => true).ToList();

        public Service Get(string id) =>
            _services.Find<Service>(service => service._id == id).FirstOrDefault();

        public Service Create(Service service)
        {
            _services.InsertOne(service);
            return service;
        }

        public void Update(string id, Service serviceIn) =>
            _services.ReplaceOne(service => service._id == id, serviceIn);

        public void Remove(Service serviceIn) =>
            _services.DeleteOne(service => service._id == serviceIn._id);

        public void Remove(string id) =>
            _services.DeleteOne(service => service._id == id);

    }
}
