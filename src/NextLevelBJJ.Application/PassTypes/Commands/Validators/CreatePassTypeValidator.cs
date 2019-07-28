using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextLevelBJJ.Application.PassTypes.Commands.Validators
{
    public class CreatePassTypeValidator : AbstractValidator<CreatePassType>
    {
        public CreatePassTypeValidator()
        {
            RuleFor(pt => pt.Name).NotEmpty();
            RuleFor(pt => pt.Entries).GreaterThan(0);
            RuleFor(pt => pt.Price).GreaterThan(0);
        }
    }
}
