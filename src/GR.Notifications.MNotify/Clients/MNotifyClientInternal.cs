using System.ServiceModel;
using System.ServiceModel.Channels;
using GR.Notifications.MNotify.Configurations;
using Microsoft.Extensions.Options;
using ServiceReference;

namespace GR.Notifications.MNotify.Clients
{
    public class MNotifyClientInternal: MNotifyClient
    {
        public MNotifyClientInternal(IOptions<MNotifyOptions> options) : this(options.Value.BasicHttpsBinding,
            options.Value.EndpointAddress)
        {
            if (options.Value != null)
            {
                ClientCredentials.ClientCertificate.Certificate = options.Value.ServiceCertificate;
            }
        }

        public MNotifyClientInternal(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
        {

        }
    }
}