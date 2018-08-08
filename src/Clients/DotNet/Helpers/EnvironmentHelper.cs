using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;

namespace Meerkat.Helpers
{
    public static partial class EnvironmentHelper
    {
        public const string Unknown = "Unknown";

        // https://code.msdn.microsoft.com/How-to-determine-operating-c90d351b/sourcecode?fileId=171485&pathId=180806365
        public static string GetOperatinSystemName()
        {
#if NETSTANDARD2_0
            return RuntimeInformation.OSDescription;
#elif NET45
            return GetOsVersion45();
#endif
        }

        public static string GetOSArchitecture()
        {
#if NETSTANDARD2_0
            return Enum.GetName(typeof(Architecture), RuntimeInformation.OSArchitecture);
#elif NET45
            return Environment.Is64BitOperatingSystem ? "X64" : "X86";
#endif
        }

        // https://code.msdn.microsoft.com/How-to-determine-operating-c90d351b/sourcecode?fileId=171485&pathId=180806365
        public static string GetRuntimeVersion()
        {
#if NET45
            const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";

            using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey))
            {
                if (ndpKey != null && ndpKey.GetValue("Release") != null)
                {
                    return CheckFor45PlusVersion((int)ndpKey.GetValue("Release"));
                }
                else
                {
                    return Unknown;
                }
            }
#elif NETSTANDARD
            return RuntimeInformation.FrameworkDescription;
#endif
        }
    }
}
