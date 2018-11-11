using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class Audit : IDisposable
    {
        private static EventLog customLog = null;
        const string SourceName = "Projekat21";
        const string LogName = "21ProjectAudit";          

        static Audit()
        {
            try
            {
                if (!EventLog.SourceExists(SourceName))
                {
                    EventLog.CreateEventSource(SourceName, LogName);
                }

                customLog = new EventLog(LogName, Environment.MachineName, SourceName);
            }
            catch (Exception)
            {
                customLog = null;
                Console.WriteLine("Greska pri kreiranju log handle-a!");
            }
        }

        public static void AuthenticationSuccess(string userName)
        {
            if (customLog != null)
            {
                customLog.WriteEntry(string.Format(AuditEvents.UserAuthenticationSuccess, userName), EventLogEntryType.SuccessAudit);
            }
            else
            {
                throw new ArgumentException(string.Format("Greska prilikom upisa dogadjaja sa id-jem {0} u event log", (int)AuditEventTypes.UserAuthenticationSuccess));
            }
        }
        public static void AuthenticationFailed(string userName, string serviceName, string reason)
        {
            if (customLog != null)
            {
                customLog.WriteEntry(string.Format(AuditEvents.UserAuthenticationFailed, userName, serviceName, reason), EventLogEntryType.Error);
            }
            else
            {
                throw new ArgumentException(string.Format("Greska prilikom upisa dogadjaja sa id-jem {0} u event log", (int)AuditEventTypes.UserAuthenticationFailed));
            }
        }

        public static void AuthorizationSuccess(string userName, string serviceName)
        {
            if (customLog != null)
            {
                customLog.WriteEntry(string.Format(AuditEvents.UserAuthorizationSuccess, userName, serviceName), EventLogEntryType.SuccessAudit);
            }
            else
            {
                throw new ArgumentException(string.Format("Greska prilikom upisa dogadjaja sa id-jem {0} u event log", (int)AuditEventTypes.UserAuthorizationSuccess));
            }
        }

        public static void AuthorizationFailed(string userName, string serviceName, string reason)
        {
            if (customLog != null)
            {
                customLog.WriteEntry(string.Format(AuditEvents.UserAuthorizationFailed, userName, serviceName, reason), EventLogEntryType.Error);
            }
            else
            {
                throw new ArgumentException(string.Format("Greska prilikom upisa dogadjaja sa id-jem {0} u event log", (int)AuditEventTypes.UserAuthorizationFailed));
            }
        }

        public void Dispose()
        {
            if (customLog != null)
            {
                customLog.Dispose();
                customLog = null;
            }
        }
    }
}
