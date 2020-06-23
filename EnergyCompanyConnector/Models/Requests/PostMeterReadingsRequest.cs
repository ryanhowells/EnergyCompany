using System;
using System.Collections.Generic;

namespace EnergyCompanyConnector.cs.Models.Requests
{
    public class PostMeterReadingsRequest
    {
        public List<MeterReading> MeterReadings { get; set; }

        public class MeterReading
        {
            public string AccountId { get; set; }

            public string MeterReadValue { get; set; }

            public DateTime MeterReadingDateTime { get; set; }
        }
    }
}
