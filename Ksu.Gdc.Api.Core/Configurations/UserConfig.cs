﻿using System;

namespace Ksu.Gdc.Api.Core.Configurations
{
    public static class UserConfig
    {
        public static string UserDataStoreDir => "users";
        public static string DataStoreUrl => AppConfiguration.GetConfig("DataStoreUrl") + "/" + UserDataStoreDir;
    }
}
