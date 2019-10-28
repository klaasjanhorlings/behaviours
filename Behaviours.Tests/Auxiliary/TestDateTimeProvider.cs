using Behaviours.Abstractions;
using System;

namespace Behaviours.Tests.Auxiliary
{
    public class TestDateTimeProvider : IDateTimeProvider
    {
        public DateTime Now { get; set; }
    }
}
