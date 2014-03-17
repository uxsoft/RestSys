using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSys.Models.Exports;
using System.Collections.ObjectModel;
using System.Composition;

namespace RestSys.ClientCore.Exports
{
    [Export(typeof(IRSApiConnector<>))]
    public class ApiConnector<T> : IRSApiConnector<T> where T : IRSEntity
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

        public async Task Synchronize()
        {
            throw new NotImplementedException();
        }

        public int IndexOf(T item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public T this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public event System.Collections.Specialized.NotifyCollectionChangedEventHandler CollectionChanged;

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
    }
}
