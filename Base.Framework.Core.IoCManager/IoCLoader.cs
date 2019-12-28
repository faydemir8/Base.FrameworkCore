using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Base.Framework.Core.IoCManager.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Framework.Core.IoCManager
{
    public static class IocLoader
    {
        public static void UseIocLoader(this IServiceCollection serviceCollection)
        {
            var transientType = typeof(ITransientService);
            var scopedType = typeof(IScopedService);
            var singletonType = typeof(ISingletonService);

            var listOfAssemblies = new List<Assembly>();

            var mainAsm = Assembly.GetEntryAssembly();
            listOfAssemblies.Add(mainAsm);

            if (mainAsm != null)
                foreach (var refAsmName in mainAsm.GetReferencedAssemblies())
                {
                    listOfAssemblies.Add(Assembly.Load(refAsmName));
                }

            foreach (var assembly in listOfAssemblies)
            {
                var enumerable = assembly
                    .GetExportedTypes()?.Where(p =>
                    transientType.IsAssignableFrom(p) ||
                    scopedType.IsAssignableFrom(p) ||
                    singletonType.IsAssignableFrom(p)
                );


                foreach (var service in enumerable)
                {

                    var interfaceOfService = service.GetInterfaces().FirstOrDefault(x => x != transientType && x != scopedType && x != singletonType);

                    if (!service.IsClass) continue;
                    if (transientType.IsAssignableFrom(service))
                    {
                        if (interfaceOfService == null)
                            serviceCollection.AddTransient(service);
                        else
                            serviceCollection.AddTransient(interfaceOfService, service);
                    }

                    else if (scopedType.IsAssignableFrom(service))
                    {
                        if (interfaceOfService == null)
                            serviceCollection.AddScoped(service);
                        else
                            serviceCollection.AddScoped(interfaceOfService, service);
                    }

                    else if (singletonType.IsAssignableFrom(service))
                    {
                        if (interfaceOfService == null)
                            serviceCollection.AddSingleton(service);
                        else
                            serviceCollection.AddSingleton(interfaceOfService, service);
                    }

                }
            }

        }
    }
}
