using Application.Common.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Fluent_Validations.Instruction_Validations
{
    public class InstructionValid:AbstractValidator<GetInstructionDTO>
    {
        public InstructionValid()
        {
            RuleFor(p => p.ClientName).NotEmpty().WithMessage("{PropertyName} should be not empty. NEVER!").Length(2, 25);
            RuleFor(p => p.PickupAddress).NotEmpty().WithMessage("{PropertyName} should be not empty. NEVER!").Length(2, 50);
            RuleFor(p => p.DeliveryAddress).NotEmpty().WithMessage("{PropertyName} should be not empty. NEVER!").Length(2, 50);
            RuleFor(p => p.ClientRef).NotEmpty().WithMessage("{PropertyName} should be not empty. NEVER!").Length(2, 25);
            RuleFor(p => p.BillingRef).NotEmpty().WithMessage("{PropertyName} should be not empty. NEVER!").Length(2, 50);
        }
    }
}
