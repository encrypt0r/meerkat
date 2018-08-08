using System;

namespace Meerkat.Client.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new MeerkatClient("https://localhost:5000"))
            {
                try
                {
                    Throw();
                }
                catch (Exception e)
                {
                    var result = client.ReportAsync(e).Result;
                    Console.WriteLine(result);
                }
            }
        }

        private static void Throw()
        {
            throw new NullReferenceException();
        }
    }
}
