using EnergyCompanyDomain;
using EnergyCompanySql;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EnergyCompanyAPI.Features.Customers.MeterReadings
{
    public class Upload
    {
        public class Command : IRequest<UploadMeterReadingViewModel>
        {
            public IEnumerable<MeterReading> MeterReadings { get; set; }

            public class MeterReading
            {
                public string AccountId { get; set; }

                public string MeterReadValue { get; set; }

                public DateTime MeterReadingDateTime { get; set; }
            }
        }

        public class MeterReadingsValidator : AbstractValidator<Command.MeterReading>
        {
            public MeterReadingsValidator()
            {
                RuleFor(x => x.AccountId)
                    .NotNull()
                    .WithMessage("AccountId must be supplied.");

                RuleFor(x => x.MeterReadValue)
                       .NotNull()
                       .WithMessage("Meter Read Value must be provided.")
                       .Length(5)
                       .WithMessage("Meter Read Value must be five numbers.")
                       .Must(x => x.All(char.IsNumber))
                       .WithMessage("Meter Read Value must be five numbers.");
            }
        }

        public class Handler : IRequestHandler<Command, UploadMeterReadingViewModel>
        {
            private readonly EnergyCompanyDbContext _context;

            public Handler(EnergyCompanyDbContext context)
            {
                _context = context;
            }

            public async Task<UploadMeterReadingViewModel> Handle(Command request, CancellationToken cancellationToken)
            {
                MeterReadingsValidator validator = new MeterReadingsValidator();

                var uploadViewModel = new UploadMeterReadingViewModel();
                IEnumerable<MeterReading> meterReadings = _context.MeterReadings.ToArray();

                foreach (var meterReading in request.MeterReadings)
                {
                    IEnumerable<string> accountMeterReadings = meterReadings
                        .Where(x => x.AccountId == meterReading.AccountId)
                        .Select(x => x.MeterReadValue);

                    ValidationResult validationResult = validator.Validate(meterReading);
                    if (validationResult.IsValid && !accountMeterReadings.Contains(meterReading.MeterReadValue))
                    {
                        await _context.MeterReadings.AddAsync(
                            new MeterReading(
                                meterReading.AccountId,
                                meterReading.MeterReadingDateTime,
                                meterReading.MeterReadValue
                            ));

                        uploadViewModel.TotalSuccessCount += 1;
                    }
                    else
                    {
                        uploadViewModel.TotalFailCount += 1;
                    }
                }

                await _context.SaveChangesAsync(cancellationToken);
                return uploadViewModel;
            }
        }
    }
}
