using System;
using TogglerAdmin.Abstractions;

namespace TogglerAdmin.Domain
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
