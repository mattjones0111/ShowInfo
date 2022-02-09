namespace Process.Features.Shows
{
    using System.Threading;
    using System.Threading.Tasks;
    using Contracts.V1;
    using FluentValidation;
    using MediatR;
    using Pipeline;
    using Ports;

    public class Put
    {
        public class Command : IRequest<CommandResult>
        {
            public Show Show { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Show)
                    .NotNull();
            }
        }

        public class Handler : IRequestHandler<Command, CommandResult>
        {
            readonly IShowRepository _repository;

            public Handler(IShowRepository repository)
            {
                _repository = repository;
            }

            public async Task<CommandResult> Handle(
                Command request,
                CancellationToken cancellationToken)
            {
                await _repository.StoreAsync(request.Show);

                return CommandResult.Void;
            }
        }
    }
}
