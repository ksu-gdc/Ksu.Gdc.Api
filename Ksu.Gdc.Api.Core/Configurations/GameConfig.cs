using System;

namespace Ksu.Gdc.Api.Core.Configurations
{
    public static class GameConfig
    {
        public static string DataStoreUrl => AppConfiguration.GetConfig("DataStoreUrl") + "/portfolio/games";
    }
}
