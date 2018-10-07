using System;

namespace Ksu.Gdc.Api.Configuration
{
    public static class UserConfig
    {
        public static string DataStoreDirPath => "users";
        public static string DataStoreUrl => AppConfiguration.GetConfig("DataStoreUrl") + "/" + DataStoreDirPath;
    }
}
