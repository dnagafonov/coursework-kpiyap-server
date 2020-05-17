using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coursework_kpiyap.Models
{
    public class SpareStoreDatabaseSettings : ISpareStoreDatabaseSettings
    {
        public string SparesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ISpareStoreDatabaseSettings
    {
        string SparesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
