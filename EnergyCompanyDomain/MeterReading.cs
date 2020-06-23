using System;

namespace EnergyCompanyDomain
{
    public class MeterReading
    {
        public MeterReading(string accountId, DateTime meterReadingDateTime, string meterReadValue)
        {
            MeterReadingId = Guid.NewGuid();
            AccountId = accountId;
            MeterReadingDateTime = meterReadingDateTime;
            MeterReadValue = meterReadValue;
        }

        public Guid MeterReadingId { get; private set; }

        public string AccountId { get; private set; }

        public DateTime MeterReadingDateTime { get; private set; }

        public string MeterReadValue { get; private set; }
    }
}
