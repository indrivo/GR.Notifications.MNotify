using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;

namespace GR.Notifications.MNotify.Configurations
{
    public class MNotifyOptions
    {
         public string ServiceClientAddress { get; set; }

        /// <summary>
        /// Path of service certificate.
        /// </summary>
        public string ServiceCertificatePath { get; set; }

        /// <summary>
        /// Password for service certificate.
        /// </summary>
        public string ServiceCertificatePassword { get; set; }


        /// <summary>
        /// Service certificate.
        /// </summary>
        public X509Certificate2 ServiceCertificate { get; set; }

        /// <summary>
        /// BasicHttpsBinding
        /// </summary>
        public BasicHttpsBinding BasicHttpsBinding { get; set; }

        /// <summary>
        /// BasicHttpsBinding
        /// </summary>
        public EndpointAddress EndpointAddress { get; set; }
    }
}
