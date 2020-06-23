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

        public Guid MeterReadingId { get; set; }

        public string AccountId { get; set; }

        public DateTime MeterReadingDateTime { get; set; }

        public string MeterReadValue { get; set; }
    }
}
