using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextLevelBJJ.Application.PassTypes.Queries.Validators
{
    public class GetPassTypeValidator : AbstractValidator<GetPassType>
    {
        public GetPassTypeValidator()
        {
            RuleFor(pt => pt.Id).NotEmpty();
            RuleFor(pt => pt.Id).NotNull();
        }
    }
}
