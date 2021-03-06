namespace Process.Features.Shows
{
    using System.Threading;
    using System.Threading.Tasks;
    using Contracts.V1;
    using FluentValidation;
    using MediatR;
    using Ports;

    public class Get
    {
        public class Query : IRequest<Show[]>
        {
            public int PageNumber { get; set; }
            public int PageSize { get; set; }
        }

        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
                RuleFor(x => x.PageNumber)
                    .GreaterThan(0);

                RuleFor(x => x.PageSize)
                    .GreaterThan(0)
                    .LessThan(Constants.MaximumPageSize);
            }
        }

        public class Handler : IRequestHandler<Query, Show[]>
        {
            readonly IShowRepository _repository;

            public Handler(IShowRepository repository)
            {
                _repository = repository;
            }

            public async Task<Show[]> Handle(
                Query request,
                CancellationToken cancellationToken)
            {
                return await _repository.GetAsync(
                    request.PageNumber,
                    request.PageSize);
            }
        }
    }
}
