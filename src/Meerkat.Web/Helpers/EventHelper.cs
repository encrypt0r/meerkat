using Meerkat.Web.Models;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Meerkat.Web.Helpers
{
    public static class EventHelper
    {
        private static readonly Regex _numberRegex = new Regex("[0-9]{2,}|[0-9]+.[0-9]+", RegexOptions.Compiled);
        private static readonly Regex _hexRegex = new Regex("[A-F0-9]{4,}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public static string GetFingerprint(Event e)
        {
            // We use Rollbar's grouping algorithm:
            // https://docs.rollbar.com/docs/grouping-algorithm/

            if (e.Level == Core.Models.EventLevel.Error)
            {
                return GetErrorFingerprint(e);
            }
            else
            {
                return GetLogFingerprint(e);
            }
        }

        private static string GetErrorFingerprint(Event error)
        {
            var builder = new StringBuilder();

            foreach(var frame in error.StackTrace)
            {
                builder.Append(frame.FileName);
                builder.Append(frame.Function);
            }

            builder.Append(error.Type);

            return GetHash(builder.ToString());
        }

        private static string GetLogFingerprint(Event log)
        {
            var message = _numberRegex.Replace(log.Message, string.Empty);
            message = _hexRegex.Replace(message, string.Empty);

            return GetHash(message);
        }

        private static string GetHash(string fingerprint)
        {
            using (var hashingFunction = SHA1.Create())
            {
                var encoded = Encoding.UTF8.GetBytes(fingerprint);
                var hashed = hashingFunction.ComputeHash(encoded);

                var builder = new StringBuilder();
                foreach(var b in hashed)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
