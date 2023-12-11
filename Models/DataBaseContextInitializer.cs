using Microsoft.Extensions.DependencyInjection;
using System;
namespace NSTask.Models
{
    public static class InfraMiddlewareExtension
    {
        public static void DataBaseInitializer(this IServiceProvider service)
        {
            using (var scope = service.CreateScope())
            {
                var initialiser = scope.ServiceProvider.GetRequiredService<DataBaseInitializer>();
                initialiser.Initialize();
            }
        }
    }
}


