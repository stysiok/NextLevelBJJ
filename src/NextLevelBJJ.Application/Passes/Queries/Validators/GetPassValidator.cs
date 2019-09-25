using System;
using FluentValidation;

namespace NextLevelBJJ.Application.Passes.Queries.Validators
{
    public class GetPassValidator : AbstractValidator<GetPass>
    {
        public GetPassValidator()
        {
            RuleFor(pt => pt.Id).NotEmpty();
            RuleFor(pt => pt.Id).NotNull();
        }
    }
}
