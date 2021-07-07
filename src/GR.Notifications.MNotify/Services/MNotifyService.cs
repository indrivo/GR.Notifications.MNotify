using System;
using System.Diagnostics;
using System.ServiceModel;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation;
using GR.Notifications.MNotify.Interfaces;
using GR.Notifications.MNotify.Models;
using ServiceReference;

namespace GR.Notifications.MNotify.Services
{
    public class MNotifyService : IMNotifyService
    {
        private readonly IMNotify _mNotify;

        //Validators
        private readonly IValidator<MNotifyNotification> _validator;

        public MNotifyService(IMNotify mNotify, IValidator<MNotifyNotification> validator)
        {
            _mNotify = mNotify;
            _validator = validator;
        }

        public virtual async Task<MNotifyResult<string>> SentNotificationAsync(string notificationType,
            MNotifyPerson sender, MNotifyPerson recipient, string subject, string content)
            => await SentNotificationAsync(new MNotifyNotification
            {
                NotificationType = notificationType,
                Recipient = recipient,
                Sender = sender
            });

        public virtual async Task<MNotifyResult<string>> SentNotificationAsync(MNotifyNotification notification)
        {
            if (notification == null) throw new ArgumentNullException(nameof(notification));

            var validationResult = await ValidateNotificationAsync(notification);
            if (!validationResult.Success) return validationResult;
            var result = new MNotifyResult<string>();
            try
            {
                var rawSender = JsonSerializer.Serialize(notification.Sender);
                var rawRecipient = JsonSerializer.Serialize(notification.Recipient);
                var notificationId = await _mNotify.PostSimpleNotificationAsync(notification.NotificationType, rawSender, rawRecipient,
                    notification.Subject, NotificationContentType.Html, notification.Content);
                result.Success = true;
                result.Data = notificationId;
            }
            catch (FaultException ex)
            {
                result.HasException = true;
                result.Exception = ex;
                result.Errors.Add(ex.Message);
                Debug.WriteLine("Simple Notification fault: {0}: {1}", ex.Code.Name, ex.Reason);
            }
            catch (Exception ex)
            {
                result.HasException = true;
                result.Exception = ex;
                result.Errors.Add(ex.Message);
                Debug.WriteLine("Simple Notification fault: {0}", ex.Message);
            }

            return result;
        }

        private async Task<MNotifyResult<string>> ValidateNotificationAsync(MNotifyNotification notification)
        {
            var validationResult = await _validator.ValidateAsync(notification);
            if (validationResult.IsValid) return new MNotifyResult<string> { Success = true };
            var failResult = new MNotifyResult<string>();

            foreach (var error in validationResult.Errors)
            {
                failResult.Errors.Add(error.ErrorMessage);
            }

            return failResult;
        }

        public virtual async Task<MNotifyResult<string>> SentNotificationAsync(NotificationRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var result = new MNotifyResult<string>();
            try
            {
                var notificationId = await _mNotify.PostNotificationRequestAsync(request);
                result.Success = true;
                result.Data = notificationId;
            }
            catch (FaultException ex)
            {
                result.HasException = true;
                result.Exception = ex;
                result.Errors.Add(ex.Message);
                Debug.WriteLine("Complex Notification fault: {0}: {1}", ex.Code.Name, ex.Reason);
            }
            catch (Exception ex)
            {
                result.HasException = true;
                result.Exception = ex;
                result.Errors.Add(ex.Message);
                Debug.WriteLine("Complex Notification fault: {0}", ex.Message);
            }

            return result;
        }

        public virtual async Task<MNotifyResult<NotificationStatus[]>> GetNotificationStatusAsync(string notificationId)
        {
            var result = new MNotifyResult<NotificationStatus[]>();
            try
            {
                result.Data = await _mNotify.GetNotificationStatusAsync(notificationId);
                result.Success = true;
            }
            catch (FaultException ex)
            {
                result.HasException = true;
                result.Exception = ex;
                result.Errors.Add(ex.Message);
            }
            catch (Exception ex)
            {
                result.HasException = true;
                result.Exception = ex;
                result.Errors.Add(ex.Message);
            }

            return result;
        }
    }
}