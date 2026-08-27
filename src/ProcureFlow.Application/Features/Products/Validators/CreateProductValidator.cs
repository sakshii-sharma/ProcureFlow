using FluentValidation;
using ProcureFlow.Application.Features.Products.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcureFlow.Application.Features.Products.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Product name is required.")
                .MaximumLength(150)
                .WithMessage("Product name cannot exceed 150 characters.");

            RuleFor(x => x.SKU)
                .NotEmpty()
                .WithMessage("SKU is required.")
                .MaximumLength(50)
                .WithMessage("SKU cannot exceed 50 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .WithMessage("Category is required.");
        }
    }
}
