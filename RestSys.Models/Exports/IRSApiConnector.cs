using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSys.Models.Exports
{
    public interface IRSApiConnector<T> where T : IRSEntity
    {
        IRSApiConnector<T> AttachPersistencyProvider(IRSPersistencyProvider persistencyProvider);
        IRSApiConnector<T> AttachWebApiEndpoint(string url);

        Task Update(T t);
        Task Add(T t);
        Task Find(int id);
        Task Delete(int id);
        Task<ObservableCollection<T>> Get();
        Task Synchronize();
        //or something like that
    }
}
