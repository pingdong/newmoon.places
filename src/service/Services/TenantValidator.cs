﻿using System.Threading.Tasks;
using PingDong.Newmoon.Places.Core;

namespace PingDong.Newmoon.Places.Service
{
    public class TenantValidator : ITenantValidator
    {
        public Task<bool> IsValidAsync(string tenantId)
        {
            // Demo purpose only
            // In real world, TenantId should check against DB
            return Task.FromResult(!string.IsNullOrWhiteSpace(tenantId));
        }
    }
}
