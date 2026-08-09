using FluentValidation;
using ProcureFlow.Application.Features.Categories.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcureFlow.Application.Features.Categories.Validators
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryValidator() 
        { 
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is a required field")
                .MaximumLength(100).WithMessage("Category Name cannot exceed 100 characters");

            RuleFor(x => x.Description).MaximumLength(500).WithMessage("Description cannot exceed 500 characters");
        }
    }
}
