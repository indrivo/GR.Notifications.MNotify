using FluentValidation;
using GR.Notifications.MNotify.Clients;
using GR.Notifications.MNotify.Configurations;
using GR.Notifications.MNotify.Interfaces;
using GR.Notifications.MNotify.Models;
using GR.Notifications.MNotify.Services;
using GR.Notifications.MNotify.Validations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ServiceReference;

namespace GR.Notifications.MNotify
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMNotify(this IServiceCollection services, IConfiguration configuration)
        {
            //Configurations
            services.Configure<MNotifyOptions>(configuration.GetSection(nameof(MNotifyOptions)));
            services.AddSingleton<IPostConfigureOptions<MNotifyOptions>, MNotifyPostConfigureOptions>();
            
            //Services
            services.AddScoped<IMNotify, MNotifyClientInternal>();
            services.AddScoped<IMNotifyService, MNotifyService>();

            //Validators
            services.AddScoped<IValidator<MNotifyNotification>, MNotifyNotificationValidator>();
            return services;
        }
    }
}