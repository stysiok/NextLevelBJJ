using NextLevelBJJ.Application;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace NextLevelBJJ.Infrastructure.Dispatchers
{
    public class Dispatcher : IDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public Dispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task SendAsync<T>(T command) where T : ICommand
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<T>>();
                return handler.HandleAsync(command);
            }
        }

        public Task<T> QueryAsync<T>(IQuery<T> query)
        {
            using(var scope = _serviceProvider.CreateScope())
            {
                var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(T));
                dynamic handler = scope.ServiceProvider.GetRequiredService(handlerType);

                return handler.HandleAsync((dynamic)query);
            }
        }
    }
}
