using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSys.Models.Exports
{
    public interface IRSApiConnector<T> : IList<T>, INotifyCollectionChanged, INotifyPropertyChanged where T : IRSEntity
    {
        IRSApiConnector<T> AttachPersistencyProvider(IRSPersistencyProvider persistencyProvider);
        IRSApiConnector<T> AttachWebApiEndpoint(string url);

        T Find(int id);
        Task Synchronize();
    }
}
