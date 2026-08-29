using FluentValidation;
using ProcureFlow.Application.Features.Suppliers.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcureFlow.Application.Features.Suppliers.Validators
{
    public class CreateSupplierValidator : AbstractValidator<CreateSupplierDto>
    {
        public CreateSupplierValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(150);

            RuleFor(x => x.Phone)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.Address)
                .MaximumLength(250);
        }
    }
}
