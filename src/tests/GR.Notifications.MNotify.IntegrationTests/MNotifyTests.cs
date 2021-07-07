using System;
using System.IO;
using System.Threading.Tasks;
using GR.Notifications.MNotify.Interfaces;
using GR.Notifications.MNotify.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GR.Notifications.MNotify.IntegrationTests
{
    [TestClass]
    public class MNotifyTests
    {
        private IConfiguration _configuration;
        private IServiceProvider _serviceProvider;

        [TestInitialize]
        public void Initialize()
        {
            var confBuilder = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(AppContext.BaseDirectory, "appsettings.json"), false)
                .AddEnvironmentVariables();

            _configuration = confBuilder.Build();

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddMNotify(_configuration);

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        [TestMethod]
        public async Task Notification_Should_Be_Sent()
        {
            var mNotifyService = _serviceProvider.GetRequiredService<IMNotifyService>();

            var sendResult = await mNotifyService.SentNotificationAsync(new MNotifyNotification
            {
                Sender = new MNotifyPerson("Lupei Nicolae", "nicolae.lupei@indrivo.com"),
                Recipient = new MNotifyPerson("Lupei Nicolae", "nicolae.lupei@indrivo.com"),
                NotificationType = "Test",
                Subject = "Test notification",
                Content = "Test notification"
            });

            Assert.IsTrue(sendResult.Success, sendResult.ErrorMessage);
        }
    }
}