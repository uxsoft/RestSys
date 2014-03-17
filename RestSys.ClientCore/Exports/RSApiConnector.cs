using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSys.Models.Exports;
using System.Collections.ObjectModel;

namespace RestSys.ClientCore.Exports
{
    public class RSApiConnector<T> : IRSApiConnector<T> where T : IRSEntity
    {
        private IRSPersistencyProvider PersistencyProvider { get; set; }
        private string WebApiEndpoint { get; set; }
        private ObservableCollection<T> Collection { get; set; }

        public IRSApiConnector<T> AttachPersistencyProvider(IRSPersistencyProvider persistencyProvider)
        {
            PersistencyProvider = persistencyProvider;
            return this;
        }

        public IRSApiConnector<T> AttachWebApiEndpoint(string url)
        {
            WebApiEndpoint = url;
            return this;
        }

        public Task Update(T t)
        {
            throw new NotImplementedException();
        }

        public Task Add(T t)
        {
            throw new NotImplementedException();
        }

        public Task Find(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ObservableCollection<T>> Get()
        {
            throw new NotImplementedException();
        }

        public Task Synchronize()
        {
            throw new NotImplementedException();
        }
    }
}
