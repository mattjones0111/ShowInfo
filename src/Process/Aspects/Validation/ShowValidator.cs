namespace Process.Aspects.Validation
{
    using Contracts.V1;
    using FluentValidation;

    public class ShowValidator : AbstractValidator<Show>
    {
        public ShowValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.Cast)
                .NotEmpty();

            RuleForEach(x => x.Cast)
                .SetValidator(new CastMemberValidator());
        }
    }

    public class CastMemberValidator : AbstractValidator<CastMember>
    {
        public CastMemberValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}
