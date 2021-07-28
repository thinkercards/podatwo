using CCMERP.Service.Contract;
using System;

namespace CCMERP.Service.Implementation
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}