namespace Process.Pipeline
{
    using System.Collections.Generic;
    using System.Linq;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
    using Aspects.Validation;
    using FluentValidation;
    using FluentValidation.Results;

    public sealed class PipelineBehaviour<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        readonly IEnumerable<IValidator<TRequest>> _validators;

        public PipelineBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            ValidationFailure[] errors = _validators
                .Select(x => x.Validate(request))
                .SelectMany(x => x.Errors)
                .ToArray();

            if (errors.Any())
            {
                throw new PipelineValidationException(errors);
            }

            return await next();
        }
    }
}
