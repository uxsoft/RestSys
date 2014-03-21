using RestSys.Client.Services.EntityService;
using RestSys.Models.Exports;
using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSys.Client.Services.EntityService
{
    public static class ServiceExtensions
    {
        public static async Task<DataServiceResponse> SaveChangesAsync(this RSDbContext Db)
        {
            TaskFactory tf = new TaskFactory();
            return await tf.FromAsync(Db.BeginSaveChanges(null, null), iar => Db.EndSaveChanges(iar));
        }

        public async static Task<IEnumerable<T>> Get<T>(this IQueryable<T> query)
        {
            DataServiceQuery<T> dqs = (DataServiceQuery<T>)(query);
            return await dqs.Get();
        }

        public async static Task<IEnumerable<T>> Get<T>(this DataServiceQuery<T> dqs)
        {
            TaskFactory<IEnumerable<T>> tf = new TaskFactory<IEnumerable<T>>();
            return await tf.FromAsync(dqs.BeginExecute(null, null), iar => dqs.EndExecute(iar));
        }
    }
}
