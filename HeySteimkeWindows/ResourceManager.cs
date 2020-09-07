using HeySteimke.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeySteimkeWindows
{
    public static class ResourceManager
    {
        static IDataStore dataStore;
        public static IDataStore DataStore
        {
            get
            {
                if (dataStore == null)
                {
                    var tmp_ds = new DataStore();
                    var tsk = Task.Run(async ()=> await tmp_ds.LoadConfig());
                    tsk.Wait();
                    dataStore = tmp_ds;
                }
                return dataStore;
            }
        }
    }
}
