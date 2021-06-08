using Microsoft.Extensions.DependencyInjection;
using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Interfaces.Repositories.Comments;
using Yamaanco.Application.Interfaces.Repositories.Notifications;
using Yamaanco.Infrastructure.EF.Persistence.MSSQL.Common;
using Yamaanco.Infrastructure.EF.Persistence.MSSQL.Repositories.CommentsRepository;
using Yamaanco.Infrastructure.EF.Persistence.MSSQL.Repositories.NotificationsRepository;

namespace Yamaanco.Infrastructure.EF.Persistence.MSSQL.Extentions
{
    public static class ConfigureEfContainerExtentions
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(ICommentsRepository), typeof(CommentsRepository));
            services.AddTransient(typeof(INotificationsRepository), typeof(NotificationsRepository));
        }
    }
}