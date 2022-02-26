using Microsoft.Extensions.Configuration;
using System;

namespace HiringCodingTestApis.Core.Constants
{
    public class ConnectionManager
    {
        public static string ConnectionString
        {
            get
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
                return configuration.GetConnectionString("DefaultConnection");
            }
        }
    }
}
