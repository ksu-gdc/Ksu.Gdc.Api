using System;

namespace Ksu.Gdc.Api.Configuration
{
    public static class GameConfig
    {
        public static string DataStoreDirPath => PortfolioConfig.DataStoreDirPath + "/games";
        public static string DataStoreUrl => PortfolioConfig.DataStoreUrl + "/" + DataStoreDirPath;
    }
}
