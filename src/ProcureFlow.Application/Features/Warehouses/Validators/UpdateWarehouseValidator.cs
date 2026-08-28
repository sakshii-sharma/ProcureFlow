using FluentValidation;
using ProcureFlow.Application.Features.Warehouses.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcureFlow.Application.Features.Warehouses.Validators
{
    public class UpdateWarehouseValidator : AbstractValidator<UpdateWarehouseDto>
    {
        public UpdateWarehouseValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Location)
                .NotEmpty()
                .MaximumLength(250);
        }
    }
}
