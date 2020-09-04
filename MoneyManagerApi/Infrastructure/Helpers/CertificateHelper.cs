using System.Security.Cryptography.X509Certificates;
using ExpenseManagerApi.Infrastructure.Exceptions;

namespace MoneyManagerApi.Infrastructure.Helpers
{
    public static class CertificateHelper
    {
        public static X509Certificate2 GetCertificate(string thumbprint)
        {
            var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);

            try
            {
                store.Open(OpenFlags.ReadOnly);
                var certificateCollections = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);

                if (certificateCollections.Count == 0)
                {
                    throw new CertificateException();
                }

                return certificateCollections[0];
            }
            finally
            {
                store.Close();
            }
        }
    }
}
