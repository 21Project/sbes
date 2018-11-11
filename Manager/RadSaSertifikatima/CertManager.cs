using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class CertManager
    {
        /// <summary>
		/// Get a certificate with the specified subject name from the predefined certificate storage
		/// Only valid certificates should be considered
		/// </summary>
		/// <param name="storeName"></param>
		/// <param name="storeLocation"></param>
		/// <param name="subjectName"></param>
		/// <returns> The requested certificate. If no valid certificate is found, returns null. </returns>
		public static X509Certificate2 GetCertificateFromStorage(StoreName storeName, StoreLocation storeLocation, string subjectName)
        {
            X509Store store = new X509Store(storeName, storeLocation);
            store.Open(OpenFlags.ReadOnly);

            X509Certificate2Collection certCollection = store.Certificates.Find(X509FindType.FindBySubjectName, subjectName, true);

            foreach (X509Certificate2 c in certCollection)
            {
				if (c.SubjectName.Name.Contains(string.Format("CN={0}", subjectName)))
                {
					
                    return c;
                }
            }
			
            return null;
        }


        /// <summary>
        /// Get a certificate from the specified .pfx file		
        /// </summary>
        /// <param name="fileName"> .pfx file name </param>
        /// <returns> The requested certificate. If no valid certificate is found, returns null. </returns>
        public static X509Certificate2 GetCertificateFromFile(string fileName)
        {
            X509Certificate2 certificate = null;

            Console.Write("Insert password for the private key: ");
            string pwd = Console.ReadLine();

            SecureString secPwd = new SecureString();
            foreach (char c in pwd)
            {
                secPwd.AppendChar(c);
            }
            pwd = String.Empty;

            try
            {
                certificate = new X509Certificate2(fileName, secPwd);
            }
            catch (Exception e)
            {
                Console.WriteLine("Erroro while trying to GetCertificateFromFile {0}. ERROR = {1}", fileName, e.Message);
            }

            return certificate;
        }

        public static X509Certificate2 GetCertificate(IIdentity identity)
        {
            try
            {
                // X509Identity is an internal class, so we cannot directly access it
                Type x509IdentityType = identity.GetType();

                // The certificate is stored inside a private field of this class
                FieldInfo certificateField = x509IdentityType.GetField("certificate", BindingFlags.Instance | BindingFlags.NonPublic);

                return (X509Certificate2)certificateField.GetValue(identity);
            }
            catch (Exception)
            {
                return null;
            }
        }


    }
}
