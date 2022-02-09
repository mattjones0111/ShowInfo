namespace Process.Configuration
{
    using FluentValidation;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using Pipeline;

    public static class DependencyResolution
    {
        public static IServiceCollection AddFeatures(
            this IServiceCollection services)
        {
            services.AddMediatR(typeof(IProcessLivesHere));

            services.AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(PipelineBehaviour<,>));

            services.Scan(a =>
                a.FromAssemblyOf<IProcessLivesHere>()
                    .AddClasses(c => c.AssignableTo(typeof(IValidator<>)))
                    .AsImplementedInterfaces());

            return services;
        }
    }
}
