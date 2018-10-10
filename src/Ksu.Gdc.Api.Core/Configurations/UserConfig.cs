namespace Ksu.Gdc.Api.Core.Configurations
{
    public static class UserConfig
    {
        public static string DataStoreDirPath => "users";
        public static string DataStoreUrl => AppConfiguration.GetConfig("DataStoreUrl") + "/" + DataStoreDirPath;
    }
}
