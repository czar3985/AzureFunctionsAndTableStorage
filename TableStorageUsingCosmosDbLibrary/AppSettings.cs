using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;

namespace TableStorageUsingCosmosDbLibrary
{
    public class AppSettings
    {
        public string StorageConnectionString { get; set; }

        public static AppSettings LoadAppSettings(ExecutionContext context)
        {
            IConfigurationRoot configRoot = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            AppSettings appSettings = configRoot.Get<AppSettings>();
            return appSettings;
        }
    }
}
