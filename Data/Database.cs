using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    /// <summary>
    /// Class that will serve as the connection to the database (the data layer).
    /// 
    /// In a production environment, this would be a class that handles querying of an ADO SQL connection or an Entity Framework instance
    /// For testing, just using a hard-coded dataset
    /// </summary>
    class Database
    {

        //Our "database". A DataSet that is created as a Singleton so that there is only ever one instance
        private static ExpertData _db = new ExpertData();

        /// <summary>
        /// The "database"
        /// </summary>
        public static ExpertData db { get { return _db; } }
    }
}
