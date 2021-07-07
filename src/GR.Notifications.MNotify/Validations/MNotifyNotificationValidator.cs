using FluentValidation;
using GR.Notifications.MNotify.Models;

namespace GR.Notifications.MNotify.Validations
{
    public class MNotifyNotificationValidator : AbstractValidator<MNotifyNotification>
    {
        public MNotifyNotificationValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Recipient)
                .NotNull();

            RuleFor(x => x.Sender)
                .NotNull();

            RuleFor(x => x.Sender.Email)
                .NotEmpty()
                .MaximumLength(400);

            RuleFor(x => x.Sender.Name)
                .NotEmpty()
                .MaximumLength(400);

            RuleFor(x => x.Recipient.Name)
                .NotEmpty()
                .MaximumLength(400);

            RuleFor(x => x.Recipient.Email)
                .NotEmpty()
                .MaximumLength(400);

            RuleFor(x => x.NotificationType)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Content)
                .NotEmpty();

            RuleFor(x => x.Subject)
                .NotEmpty()
                .MaximumLength(200);
        }
    }
}