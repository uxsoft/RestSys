using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSys.Models.Exports;
using System.Collections.ObjectModel;
using System.Composition;
using System.Net.Http;
using RestSys.Models;

namespace RestSys.ClientCore.Exports
{
    [Export(typeof(IRSApiConnector<>))]
    public class NaiveApiConnector<T> : IRSApiConnector<T> where T : IRSEntity
    {
        public NaiveApiConnector()
        {
            Collection = new ObservableCollection<T>();
            Collection.CollectionChanged += Collection_CollectionChanged;
        }

        void Collection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
                CollectionChanged(sender, e);
        }

        private IRSPersistencyProvider PersistencyProvider { get; set; }
        private HttpClient Client { get; set; }
        private ObservableCollection<T> Collection { get; set; }
        private T LastInsertedItem { get; set; }

        public IRSApiConnector<T> AttachPersistencyProvider(IRSPersistencyProvider persistencyProvider)
        {
            PersistencyProvider = persistencyProvider;
            return this;
        }

        public IRSApiConnector<T> AttachWebApiEndpoint(string url)
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri(url, UriKind.Absolute);
            return this;
        }

        public void Test()
        {
            bool success = true;

            NaiveApiConnector<RSStyle> ac1 = new NaiveApiConnector<RSStyle>();
            NaiveApiConnector<RSStyle> ac2 = new NaiveApiConnector<RSStyle>();
            ac1.AttachWebApiEndpoint("http://localhost:56345/Api/Styles/");
            ac2.AttachWebApiEndpoint("http://localhost:56345/Api/Styles/");

            RSStyle o1 = new RSStyle() { Notes = "Test-o1" };
            RSStyle o2 = new RSStyle() { Notes = "Test-o2" };
            RSStyle o3 = new RSStyle() { Notes = "Test-o3" };
            RSStyle o4 = new RSStyle() { Notes = "Test-o4" };

            ac1.Synchronize();
            ac2.Synchronize();

            ac1.Add(o1);
            o1 = ac1.LastInserted();
            ac2.Add(o2);
            o2 = ac2.LastInserted();

            ac1.Synchronize();
            ac2.Synchronize();

            ac1.Last().Notes += "Edited";


            List<RSStyle> toRemove = new List<RSStyle>();
            foreach (RSStyle s in ac1)
                if (s.Notes.StartsWith("Test-"))
                    toRemove.Add(s);

            foreach (RSStyle s in toRemove)
                ac1.Remove(s);

            ac1.Synchronize();
            ac2.Synchronize();


        }

        public async Task Synchronize()
        {
            HttpResponseMessage message = Client.GetAsync("").Result;
            if (message.IsSuccessStatusCode)
            {
                IEnumerable<T> onlinets = await message.Content.ReadAsAsync<IEnumerable<T>>();
                foreach (T onlinet in onlinets)
                    if (!Contains(onlinet))
                    {//Add
                        Collection.Add(onlinet);
                    }
                    else
                    {//Update    
                        //TODO update if changed
                        //T offlinet = Find(onlinet.Id);
                        //onlinet.SyncPropertiesTo(offlinet);
                    }

                //Delete
                foreach (T offlinet in Collection)
                    if (!onlinets.Any(t => t.Id == offlinet.Id))
                    {
                        offlinet.PropertyChanged -= item_PropertyChanged;
                        Collection.Remove(offlinet);
                    }
            }
        }

        public int IndexOf(T item)
        {
            return Collection.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            HttpResponseMessage message = Client.PostAsJsonAsync("", item).Result;
            if (message.IsSuccessStatusCode)
            {
                int newId = message.Content.ReadAsAsync<int>().Result;

                item.Id = newId;
                Collection.Insert(index, item);
                item.PropertyChanged += item_PropertyChanged;
                LastInsertedItem = item;
            }
        }

        public void RemoveAt(int index)
        {
            T item = Collection[index];
            Remove(item);
        }

        public T this[int index]
        {
            get
            {
                return Collection[index];
            }
            set
            {
                if (Collection[index] == null)
                    Add(value);
                else if (Collection[index].Id == value.Id)
                {
                    value.SyncPropertiesTo(Collection[index]);
                }
                else
                {
                    throw new NotImplementedException("Overwriting an existing item");
                }
            }
        }

        public void Add(T item)
        {
            HttpResponseMessage message = Client.PostAsJsonAsync("", item).Result;
            if (message.IsSuccessStatusCode)
            {
                int newId = message.Content.ReadAsAsync<int>().Result;

                item.Id = newId;
                Collection.Add(item);
                item.PropertyChanged += item_PropertyChanged;
                LastInsertedItem = item;
            }
            else throw new HttpRequestException("Failed to add the item, changes will be lost");
        }

        async void item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            T item = (T)sender;
            HttpResponseMessage message = await Client.PutAsJsonAsync(item.Id.ToString(), item);
            if (!message.IsSuccessStatusCode)
            {
                throw new NotImplementedException("Update failed, the changes will be lost");
            }
        }

        public void Clear()
        {
            //Offline Only
            Collection.Clear();
        }

        public bool Remove(T item)
        {
            if (item == null)
                return false;

            HttpResponseMessage message = Client.DeleteAsync(item.Id.ToString()).Result;
            if (message.IsSuccessStatusCode)
            {
                item.PropertyChanged -= item_PropertyChanged;
                Collection.Remove(item);
                return true;
            }
            return false;
        }

        public bool Contains(T item)
        {
            return Collection.Any(i => i.Id.Equals(item.Id));
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            foreach (T t in Collection)
                array[arrayIndex++] = t;
        }

        public int Count
        {
            get { return Collection.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Collection.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.IEnumerable)Collection).GetEnumerator();
        }

        public event System.Collections.Specialized.NotifyCollectionChangedEventHandler CollectionChanged;

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public T Find(int id)
        {
            return Collection.SingleOrDefault(i => i.Id.Equals(id));
        }

        public T LastInserted()
        {
            return LastInsertedItem;
        }
    }
}
