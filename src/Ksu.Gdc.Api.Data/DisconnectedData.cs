using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using Ksu.Gdc.Api.Data.DbContexts;

namespace Ksu.Gdc.Api.Data
{
    public class DisconnectedData
    {
        private readonly KsuGdcContext _ksuGdcContext;

        public DisconnectedData(KsuGdcContext ksuGdcContext)
        {
            _ksuGdcContext = ksuGdcContext;
            _ksuGdcContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
    }
}
