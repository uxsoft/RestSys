using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace RestSys.Client
{
    public static class DependencyInjection
    {
        static DependencyInjection()
        {
            Configuration = new ContainerConfiguration()
            .WithAssembly(typeof(DependencyInjection).GetTypeInfo().Assembly);
        }

        public static ContainerConfiguration Configuration { get; set; }

        public static void DependencyInjection(this object objectWithLooseImports)
        { 
            if(Configuration!=null)
                using (var container = Configuration.CreateContainer())
                {
                    container.SatisfyImports(objectWithLooseImports);
                }
        }
    }
}
