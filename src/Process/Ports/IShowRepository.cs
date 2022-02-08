namespace Process.Ports
{
    using System.Threading.Tasks;
    using Contracts.V1;

    public interface IShowRepository
    {
        Task<Show[]> GetAsync(int pageNumber, int pageSize);
    }
}
