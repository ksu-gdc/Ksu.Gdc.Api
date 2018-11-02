namespace Ksu.Gdc.Api.Core.Configurations
{
    public static class GroupConfig
    {
        public static string DataStoreDirPath => "groups";
        public static string DataStoreUrl => AppConfiguration.GetConfig("DataStoreUrl") + "/" + DataStoreDirPath;
    }
}
