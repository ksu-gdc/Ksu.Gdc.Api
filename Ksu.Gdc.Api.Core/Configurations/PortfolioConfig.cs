using System;

namespace Ksu.Gdc.Api.Core.Configurations
{
    public static class PortfolioConfig
    {
        public static string DataStoreDirPath => "portfolio";
        public static string DataStoreUrl => AppConfiguration.GetConfig("DataStoreUrl") + "/" + DataStoreDirPath;
    }
}
