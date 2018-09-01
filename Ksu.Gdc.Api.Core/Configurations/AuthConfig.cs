using System;

namespace Ksu.Gdc.Api.Core.Configurations
{
    public static class AuthConfig
    {
        public static string LoginUrl = AppConfiguration.GetConfig("WebApp_Url") + "/login";
        public static string LogoutUrl = AppConfiguration.GetConfig("WebApp_Url") + "/logout";
    }
}
