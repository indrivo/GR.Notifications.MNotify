using System.Threading.Tasks;
using GR.Notifications.MNotify.Models;
using ServiceReference;

namespace GR.Notifications.MNotify.Interfaces
{
    public interface IMNotifyService
    {
        /// <summary>
        /// Send notification
        /// </summary>
        /// <param name="notificationType"></param>
        /// <param name="sender"></param>
        /// <param name="recipient"></param>
        /// <param name="subject"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        Task<MNotifyResult<string>> SentNotificationAsync(string notificationType, MNotifyPerson sender, MNotifyPerson recipient, string subject, string content);

        /// <summary>
        /// Send complex notification
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<MNotifyResult<string>> SentNotificationAsync(NotificationRequest request);

        /// <summary>
        /// Send notification
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        Task<MNotifyResult<string>> SentNotificationAsync(MNotifyNotification notification);

        /// <summary>
        /// Get notification status
        /// </summary>
        /// <param name="notificationId"></param>
        /// <returns></returns>
        Task<MNotifyResult<NotificationStatus[]>> GetNotificationStatusAsync(string notificationId);
    }
}