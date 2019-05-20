using System;

namespace TogglerAdmin.Abstractions
{
    public interface ITimeProvider
    {
        DateTime UtcNow { get; }
    }
}
