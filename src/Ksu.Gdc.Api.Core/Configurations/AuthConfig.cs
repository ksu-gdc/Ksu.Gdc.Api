namespace Ksu.Gdc.Api.Core.Configurations
{
    public static class AuthConfig
    {
        public static string LoginUrl => $"{AppConfiguration.GetConfig("App_Url")}/login";
        public static string LogoutUrl => $"{AppConfiguration.GetConfig("App_Url")}/logout";
    }
}
