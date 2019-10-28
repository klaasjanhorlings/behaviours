using System;

namespace Behaviours.Abstractions
{
    public interface IDateTimeProvider
    {
        public DateTime Now { get; }
    }
}
