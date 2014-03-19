using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace RestSys.Models.Exports
{
    public static class SyncExtensions
    {
        public static void SyncPropertiesTo<T>(this T from, T to) where T : IRSEntity
        {
            foreach (var property in typeof(T).GetRuntimeProperties())
            {
                object fromP = property.GetValue(from);
                object toP = property.GetValue(to);
                if (!fromP.Equals(toP))
                    property.SetValue(to, fromP);
            }
        }
    }
}
