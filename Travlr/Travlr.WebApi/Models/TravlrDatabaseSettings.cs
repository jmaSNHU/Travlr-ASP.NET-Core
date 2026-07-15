namespace Travlr.WebApi.Models
{
    public class TravlrDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string TripsCollectionName { get; set; } = null!;

        public string UsersCollectionName { get; set; } = null!;
    }
}
