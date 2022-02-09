namespace Process.Adapters
{
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts.V1;
    using Ports;
    using Utilities;

    public sealed class InMemoryShowRepository : IShowRepository
    {
        readonly ConcurrentDictionary<int, Show> _dictionary = new();

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

        public Task StoreAsync(Show show)
        {
            _dictionary[show.Id] = show;

            return Task.CompletedTask;
        }
    }
}
