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
        void AttachPersistencyProvider(IRSPersistencyProvider persistencyProvider);

        Task Update<T>(T t);
        Task Add<T>(T t);
        Task Find<T>(int id);
        Task Delete<T>(int id);
        Task<ObservableCollection<T>> Get();
        Task Synchronize();
        //or something like that
    }
}
