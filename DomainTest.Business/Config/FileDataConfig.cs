using System.Configuration;
using System.IO;
using System;

namespace DomainTest.Business.Config
{
    public static class FileDataConfig
    {
        public static string ClientSource
        {
           get {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                    AppDomain.CurrentDomain.RelativeSearchPath ?? string.Empty,
                                    ConfigurationManager.AppSettings["ClientSource"]);
            }
        }
    }
}
