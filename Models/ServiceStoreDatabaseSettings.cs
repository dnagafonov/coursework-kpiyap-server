using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coursework_kpiyap.Models
{
    public class ServiceStoreDatabaseSettings : IServiceStoreDatabaseSettings
    {
        public string ServicesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IServiceStoreDatabaseSettings
    {
        string ServicesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
