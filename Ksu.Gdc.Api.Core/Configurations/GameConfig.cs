using System;

namespace Ksu.Gdc.Api.Core.Configurations
{
    public static class GameConfig
    {
        public static string DataStoreDirPath => PortfolioConfig.DataStoreDirPath + "/games";
        public static string DataStoreUrl => PortfolioConfig.DataStoreUrl + "/" + DataStoreDirPath;
    }
}
