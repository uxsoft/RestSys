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
    public static class DependencyInjectionExtensions
    {
        static DependencyInjectionExtensions()
        {
            Configuration = new ContainerConfiguration()
                .WithAssembly(typeof(DependencyInjectionExtensions).GetTypeInfo().Assembly);
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
