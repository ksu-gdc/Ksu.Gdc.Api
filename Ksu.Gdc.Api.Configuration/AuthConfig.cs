using System;

namespace Ksu.Gdc.Api.Configuration
{
    public static class AuthConfig
    {
        public static string LoginUrl => AppConfiguration.GetConfig("App_Url");
        public static string LogoutUrl => AppConfiguration.GetConfig("App_Url");
    }
}
