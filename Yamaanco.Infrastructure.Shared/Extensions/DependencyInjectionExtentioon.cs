using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yamaanco.Application.Common.Options;
using Yamaanco.Application.Interfaces;
using Yamaanco.Infrastructure.Shared.Files;
using Yamaanco.Infrastructure.Shared.ManageDateTime;
using Yamaanco.Infrastructure.Shared.Notifications;

namespace Yamaanco.Infrastructure.Shared.Extensions
{
    public static class DependencyInjectionExtentioon
    {
        public static void AddCommonInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            var appSettingsSection = config.GetSection("AppSettings");
            services.Configure<AppOptions>(appSettingsSection);

            var EmailSettingsSection = config.GetSection("EmailSettings");
            services.Configure<EmailOptions>(EmailSettingsSection);

            services.AddTransient<IDateTime, MachineDateTime>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<IElapsedTime, ElapsedTime>();
            services.AddTransient<INotificationService, NotificationService>();
        }
    }
}