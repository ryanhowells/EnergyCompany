using CsvHelper;
using CsvHelper.Excel;
using EnergyCompanyConnector.cs.Clients.Customers;
using EnergyCompanyConnector.cs.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace EnergyCompanyWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MeterReadingsClient _meterReadingsClient;

        public IndexModel(MeterReadingsClient meterReadingsClient)
        {
            _meterReadingsClient = meterReadingsClient;
        }

        [BindProperty]
        public IFormFile FormFile { get; set; }

        public int TotalFailCount { get; set; }

        public int TotalSuccessCount { get; set; }

        public bool UploadError { get; set; }

        public async Task OnPostAsync()
        {
            if (FormFile == null)
                return;

            var request = new PostMeterReadingsRequest();

            using var reader = FormFile.OpenReadStream();
            using var csv = new CsvReader(new ExcelParser(reader, "in"));
            try
            {
                var meterReadings = csv.GetRecords<MeterReading>();

                request.MeterReadings = new List<PostMeterReadingsRequest.MeterReading>();

                foreach (var meterReading in meterReadings)
                {
                    request.MeterReadings.Add(new PostMeterReadingsRequest.MeterReading
                    {
                        AccountId = meterReading.AccountId,
                        MeterReadValue = meterReading.MeterReadValue,
                        MeterReadingDateTime = DateTime.Parse(meterReading.MeterReadingDateTime)
                    });
                }
            }
            catch (Exception)
            {
                UploadError = true;
                return;
            }

            var response = await _meterReadingsClient.Upload(request, CancellationToken.None);
            TotalSuccessCount = response.TotalSuccessCount;
            TotalFailCount = response.TotalFailCount;
        }
    }
}
