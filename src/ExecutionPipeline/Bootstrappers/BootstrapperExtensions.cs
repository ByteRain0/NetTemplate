using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExecutionPipeline.Bootstrappers;

public static class BootstrapperExtensions
{
    public static void RunBootstrapping(this IServiceCollection services, List<Assembly> assemblies, IConfiguration configuration)
    {
        List<IBootstrapper> bootstrappers = new List<IBootstrapper>();
        foreach (var assembly in assemblies)
        {
            bootstrappers.AddRange(assembly.ExportedTypes.Where(x =>
                    typeof(IBootstrapper).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance).Cast<IBootstrapper>().ToList());
        }

        if (bootstrappers.Count < 5)
        {
            throw new InvalidOperationException("Not enough bootstrappers");
        }
        
        
        bootstrappers.ForEach(bootstrapper => bootstrapper.BootstrapServices(services,configuration));
    }
}