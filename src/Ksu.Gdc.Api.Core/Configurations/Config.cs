namespace Ksu.Gdc.Api.Core.Configurations
{
    public static class AuthConfig
    {
        public static string LoginUrl => $"{AppConfiguration.GetConfig("App_Url")}/login";
        public static string LogoutUrl => $"{AppConfiguration.GetConfig("App_Url")}/logout";
    }

    public static class GameConfig
    {
        public static string DataStoreDirPath => PortfolioConfig.DataStoreDirPath + "/games";
        public static string DataStoreUrl => PortfolioConfig.DataStoreUrl + "/" + DataStoreDirPath;
    }

    public static class PortfolioConfig
    {
        public static string DataStoreDirPath => "portfolio";
        public static string DataStoreUrl => AppConfiguration.GetConfig("DataStoreUrl") + "/" + DataStoreDirPath;
    }

    public static class UserConfig
    {
        public static string DataStoreDirPath => "users";
        public static string DataStoreUrl => AppConfiguration.GetConfig("DataStoreUrl") + "/" + DataStoreDirPath;
    }
}
