using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Advent
{
    class Program
    {
        public static IConfigurationRoot Configuration { get; set; }

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            var inputTarget = (Boolean.Parse(Configuration["isDebug"])) ? "scratch" : "input";
            var resourcePath = Configuration["resourcePath"].Replace("{object}", typeof(Days.Day4).Name).Replace("{input}", inputTarget);
            var input = System.IO.File.ReadAllLines(resourcePath);

            var day = new Days.Day4(input);
            day.PrintResults();
        }
    }
}
