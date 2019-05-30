using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Web;
using WebAppCargadorRips_V2.Controllers.APIS;

namespace WebAppCargadorRips_V2.Models
{

    public sealed class SafeTokenHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        private SafeTokenHandle() : base(true)
        {
        }

        [DllImport("kernel32.dll")]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr handle);

        protected override bool ReleaseHandle()
        {
            return CloseHandle(handle);
        }
    }
    public class NetworkConnection
    {
        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool LogonUser(String lpszUsername, String lpszDomain, String lpszPassword,
       int dwLogonType, int dwLogonProvider, out SafeTokenHandle phToken);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private extern static bool CloseHandle(IntPtr handle);

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public static void Impersonate(string domainName, string userName, string userPassword, Action actionToExecute)
        {
            SafeTokenHandle safeTokenHandle;
            try
            {
                const int LOGON32_PROVIDER_DEFAULT = 0;
                //This parameter causes LogonUser to create a primary token.
                const int LOGON32_LOGON_INTERACTIVE = 2;

                // Call LogonUser to obtain a handle to an access token.
                         bool returnValue = LogonUser(userName, domainName, userPassword,
                    LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT,
                    out safeTokenHandle);
                //Facade.Instance.Trace("LogonUser called.");

                if (returnValue == false)
                {
                    int ret = Marshal.GetLastWin32Error();

                }

                using (safeTokenHandle)
                {
                    // Use the token handle returned by LogonUser.
                    using (WindowsIdentity newId = new WindowsIdentity(safeTokenHandle.DangerousGetHandle()))
                    {
                        using (WindowsImpersonationContext impersonatedUser = newId.Impersonate())
                        {

                            actionToExecute();

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en conexión. Err: " + ex.Message);
                LogsController log = new LogsController(ex.ToString());
                log.createFolder();
                throw;
            }
        }
    }
}