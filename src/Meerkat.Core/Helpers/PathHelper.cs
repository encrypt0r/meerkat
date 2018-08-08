using System;
using System.Collections.Generic;
using System.Text;

namespace Meerkat.Helpers
{
    public static class PathHelper
    {
        public static string Combine(params string[] segments)
        {
            var fullPath = string.Empty;
            bool first = true;
            foreach (var s in segments)
            {
                var uniform = UniformSeperator(s);

                if (uniform.StartsWith("/") && !first)
                    uniform = uniform.Substring(1);

                if (uniform.EndsWith("/"))
                    uniform = uniform.Substring(0, uniform.Length - 1);

                if (fullPath == string.Empty)
                    fullPath = uniform;
                else
                    fullPath += $"/{uniform}";

                first = false;
            }

            return fullPath;
        }

        public static string UniformSeperator(string path)
        {
            return path.Replace('\\', '/');
        }
    }
}
