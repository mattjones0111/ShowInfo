namespace Process.Adapters
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts.V1;
    using Ports;
    using Utilities;

    public sealed class InMemoryShowRepository : IShowRepository
    {
        readonly Dictionary<string, Show> _dictionary = new();

        public Task<Show[]> GetAsync(int pageNumber, int pageSize)
        {
            Ensure.IsPositiveInteger(pageNumber, nameof(pageNumber));
            Ensure.IsPositiveInteger(pageSize, nameof(pageSize));

            Show[] result = _dictionary.Values
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToArray();

            return Task.FromResult(result);
        }

        public Task AddAsync(Show show)
        {
            _dictionary.Add(show.Name, show);

            return Task.CompletedTask;
        }
    }
}
