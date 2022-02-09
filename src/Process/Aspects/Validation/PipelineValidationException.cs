namespace Process.Aspects.Validation
{
    using System;
    using System.Linq;
    using FluentValidation.Results;

    public class PipelineValidationException : Exception
    {
        public ValidationFailure[] Errors { get; }

        public PipelineValidationException(ValidationFailure[] errors)
            : base(string.Join(", ", errors.Select(x => x.ErrorMessage)))
        {
            Errors = errors;
        }
    }
}
