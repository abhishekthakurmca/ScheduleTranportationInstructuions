using Application.Common.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Fluent_Validations.ScheduleTransporterValidations
{
    public class ScheduleValidations:AbstractValidator<ScheduleTransporterDto>
    {
        public ScheduleValidations()
        {
            RuleFor(p => p.DateScheduled).NotEmpty().WithMessage("{PropertyName} should be not empty. NEVER!");
            RuleFor(p => p.Transporter).NotEmpty().WithMessage("{PropertyName} should be not empty. NEVER!").Length(2, 20);
        }
    }
}
