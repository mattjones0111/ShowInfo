namespace Ingestor.Ports
{
    using System;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    public interface IFetchShowInformation
    {
        Task<ShowInfo[]> GetShowsAsync();

        Task<CastInfo[]> GetCastForShowAsync(int showId);
    }

    public class ShowProviderException : Exception
    {
        protected ShowProviderException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }

        public ShowProviderException(Exception innerException)
            : base("Could not fetch TV show information.", innerException)
        {
        }
    }

    public class ShowInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CastInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
