using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.Composition;
using System.Linq;
using System.Web;

namespace RestSys
{
    public static class CompositionConfig
    {
        public static ContainerConfiguration Configuration { get; set; }

        static CompositionConfig()
        {
            Configuration = new ContainerConfiguration()
            .WithAssembly(typeof(CompositionConfig).Assembly);
        }

        public static void DependencyInjection(this object objectWithLooseImports)
        {
            if (Configuration != null)
                using (var container = Configuration.CreateContainer())
                {
                    container.SatisfyImports(objectWithLooseImports);             
                }
        }
    }
}