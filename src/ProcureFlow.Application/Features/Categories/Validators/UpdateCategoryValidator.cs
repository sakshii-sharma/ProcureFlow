using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcureFlow.Application.Features.Categories.DTOs;
using System.Security.Cryptography.X509Certificates;

namespace ProcureFlow.Application.Features.Categories.Validators
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Category name is required.")
                .MaximumLength(100)
                .WithMessage("Category name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("Description cannot exceed 500 characters.");
        }
    }
}
