using System;

namespace Ksu.Gdc.Api.Core.Configurations
{
    public static class OfficerConfig
    {
        public static string ImageDataStoreUrl = AppConfiguration.GetConfig("DataStoreUrl") + "/officers/images";
    }
}
