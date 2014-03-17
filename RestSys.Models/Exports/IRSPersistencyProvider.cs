using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSys.Models.Exports
{
    public interface IRSPersistencyProvider
    {
        void Add<T>(T t) where T : IRSEntity;
        void Update<T>(T t) where T : IRSEntity;
        void Delete<T>(T t) where T : IRSEntity;
        T Find<T>(int id) where T : IRSEntity;
        IEnumerable<T> Get<T>() where T : IRSEntity;
    }
}
