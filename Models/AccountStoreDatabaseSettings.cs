using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coursework_kpiyap.Models
{
    public class AccountStoreDatabaseSettings : IAccountStoreDatabaseSettings
    {
        public string AccountsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IAccountStoreDatabaseSettings
    {
        string AccountsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
