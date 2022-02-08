namespace Contracts.V1
{
    public class Show
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CastMember[] Cast { get; set; }
    }
}
