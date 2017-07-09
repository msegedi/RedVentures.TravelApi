
namespace RedVentures.TravelApi.Core
{
    public class AppSettings
    {
        public ConnectionStringSettings ConnectionStrings { get; set; }

        public class ConnectionStringSettings
        {
            public string TravelApiDatabase { get; set; }
        }
    }
}
