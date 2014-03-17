using RestSys.Models.Exports;
using System;
using System.Collections.Generic;
using System.Composition;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;

namespace RestSys.Client.Exports
{
    [Export(typeof(IRSPersistencyProvider))]
    public class SettingsPersistenceProvider : IRSPersistencyProvider
    {
        private ApplicationDataContainer CreateContainer<T>()
        {
            return ApplicationData.Current.LocalSettings.CreateContainer(typeof(T).FullName, ApplicationDataCreateDisposition.Always);

        }

        public static string Serialize<T>(T objectToSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (StringWriter sw = new StringWriter())
            {
                xmlSerializer.Serialize(sw, objectToSerialize);
                return sw.ToString();
            }
        }

        public static T Deserialize<T>(string objectString)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (StringReader sr = new StringReader(objectString))
            {
                return (T)xmlSerializer.Deserialize(sr);
            }
        }

        public void Add<T>(T t) where T : IRSEntity
        {
            if (t != null)
            {
                var container = CreateContainer<T>();
                container.Values.Add(t.Id.ToString(), Serialize(t));
            }
        }

        public void Update<T>(T t) where T : IRSEntity
        {
            if (t != null)
            {
                var container = CreateContainer<T>();
                container.Values[t.Id.ToString()] = Serialize(t);
            }
        }

        public void Delete<T>(T t) where T : IRSEntity
        {
            if (t != null)
            {
                var container = CreateContainer<T>();
                if (container.Values.ContainsKey(t.Id.ToString()))
                    container.Values.Remove(t.Id.ToString());
            }
        }

        public IEnumerable<T> Get<T>() where T : IRSEntity
        {
            var container = CreateContainer<T>();
            return container.Values.Values.Select(o => Deserialize<T>(o.ToString()));
        }

        public T Find<T>(int id) where T : IRSEntity
        {
            var container = CreateContainer<T>();
            object value = container.Values[id.ToString()];
            if (value != null)
                return Deserialize<T>(value.ToString());
            else return null;
        }
    }
}
