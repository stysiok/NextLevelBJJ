using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextLevelBJJ.Application.PassTypes.Commands.Validators
{
    public class DeletePassTypeValidator : AbstractValidator<DeletePassType>
    {
        public DeletePassTypeValidator()
        {
            RuleFor(pt => pt.Id).NotEmpty();
            RuleFor(pt => pt.Id).NotNull();
        }
    }
}
