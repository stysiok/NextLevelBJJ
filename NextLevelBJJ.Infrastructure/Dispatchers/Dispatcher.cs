using NextLevelBJJ.Application;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;

namespace NextLevelBJJ.Infrastructure.Dispatchers
{
    public class Dispatcher : IDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Guid _userId;

        public Dispatcher(IServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor)
        {
            _serviceProvider = serviceProvider;
            _userId = httpContextAccessor.HttpContext.User?.Identity?.IsAuthenticated == true ? Guid.Parse(httpContextAccessor.HttpContext.User.Identity.Name) : Guid.Empty;
        }

        public Task SendAsync<T>(T command) where T : ICommand
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                if(command is IAuthenticatedCommand authenticatedCommand)
                {
                    authenticatedCommand.UserId = _userId;
                }

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
