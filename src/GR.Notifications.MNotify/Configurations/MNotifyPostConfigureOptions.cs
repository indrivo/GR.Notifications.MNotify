using System;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using AGE.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace GR.Notifications.MNotify.Configurations
{
    public class MNotifyPostConfigureOptions : IPostConfigureOptions<MNotifyOptions>
    {
        public void PostConfigure(string name, MNotifyOptions mNotifyOptions)
        {
            if (string.IsNullOrWhiteSpace(mNotifyOptions.ServiceClientAddress))
            {
                throw new ArgumentException("Please provide a ServiceClientAddress");
            }
            if (string.IsNullOrWhiteSpace(mNotifyOptions.ServiceCertificatePath))
            {
                throw new ArgumentException("Please provide a Certificate Path");
            }
            var certificate = new X509Certificate2Collection(CertificateLoader.Private(
                mNotifyOptions.ServiceCertificatePath,
                mNotifyOptions.ServiceCertificatePassword));

            if (certificate == null)
            {
                throw new ApplicationException("Invalid service certificate path or password");
            }

            mNotifyOptions.EndpointAddress = new EndpointAddress(mNotifyOptions.ServiceClientAddress);

            mNotifyOptions.BasicHttpsBinding = new BasicHttpsBinding
            {
                Security =
                {
                    Mode = BasicHttpsSecurityMode.Transport,
                    Transport =
                    {
                        ClientCredentialType = HttpClientCredentialType.Certificate
                    }
                },
                MaxReceivedMessageSize = 2147483647
            };

            if (certificate.Count > 0)
            {
                mNotifyOptions.ServiceCertificate = certificate[0];
            }
        }
    }
}
