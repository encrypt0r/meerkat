using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Meerkat.Helpers
{
    public static partial class EnvironmentHelper
    {
        // https://stackoverflow.com/a/2819962/7003797
        private static Dictionary<int, string> _versionNames = new Dictionary<int, string>
        {
            { 50 , "Windows 2000" },
            { 51 , "Windows XP" },
            { 52, "Windows 2003" },
            { 60, "Windows Vista" },
            { 60, "Windows 2008" },
            { 61, "Windows 7" },
            { 61, "Windows 2008 R2" },
            { 62, "Windows 8" },
            { 63 , "Windows 8.1" },
            { 100, "Windows 10" },
        };

        private static string GetOsVersion45()
        {
            var version = Environment.OSVersion.Version;
            var versionNumber = int.Parse($"{version.Major}{version.Minor}");

            if (_versionNames.ContainsKey(versionNumber))
                return $"{_versionNames[versionNumber]} - {version}";

            return version.ToString();
        }

        private static string CheckFor45PlusVersion(int releaseKey)
        {
            if (releaseKey >= 461808)
                return "4.7.2 or later";
            if (releaseKey >= 461308)
                return "4.7.1";
            if (releaseKey >= 460798)
                return "4.7";
            if (releaseKey >= 394802)
                return "4.6.2";
            if (releaseKey >= 394254)
                return "4.6.1";
            if (releaseKey >= 393295)
                return "4.6";
            if (releaseKey >= 379893)
                return "4.5.2";
            if (releaseKey >= 378675)
                return "4.5.1";
            if (releaseKey >= 378389)
                return "4.5";

            // This code should never execute. A non-null release key should mean
            // that 4.5 or later is installed.
            return Unknown;
        }

        // https://stackoverflow.com/a/12139504/7003797
        public static ProcessorArchitecture GetProcessorArchitecture()
        {
            SYSTEM_INFO si = new SYSTEM_INFO();
            GetNativeSystemInfo(ref si);
            switch (si.wProcessorArchitecture)
            {
                case PROCESSOR_ARCHITECTURE_AMD64:
                    return ProcessorArchitecture.Amd64;

                case PROCESSOR_ARCHITECTURE_IA64:
                    return ProcessorArchitecture.IA64;

                case PROCESSOR_ARCHITECTURE_INTEL:
                    return ProcessorArchitecture.X86;

                default:
                    return ProcessorArchitecture.None; // that's weird :-)
            }
        }

        [DllImport("kernel32.dll")]
        private static extern void GetNativeSystemInfo(ref SYSTEM_INFO lpSystemInfo);

        private const int PROCESSOR_ARCHITECTURE_AMD64 = 9;
        private const int PROCESSOR_ARCHITECTURE_IA64 = 6;
        private const int PROCESSOR_ARCHITECTURE_INTEL = 0;

        [StructLayout(LayoutKind.Sequential)]
        private struct SYSTEM_INFO
        {
            public short wProcessorArchitecture;
            public short wReserved;
            public int dwPageSize;
            public IntPtr lpMinimumApplicationAddress;
            public IntPtr lpMaximumApplicationAddress;
            public IntPtr dwActiveProcessorMask;
            public int dwNumberOfProcessors;
            public int dwProcessorType;
            public int dwAllocationGranularity;
            public short wProcessorLevel;
            public short wProcessorRevision;
        }
    }
}
