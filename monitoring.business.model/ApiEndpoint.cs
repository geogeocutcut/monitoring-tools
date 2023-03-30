namespace monitoring.business.model
{
    public class ApiEndpoint
    {
        public int Id { get; set; }
        public string Endpoint { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string? StatusHttp { get; set; }


        public string? Information { get; set; }


        public DateTime? LastUpdated { get; set; }
    }
}