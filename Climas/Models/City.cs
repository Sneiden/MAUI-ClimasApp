
using SQLite;

namespace Climas.Models
{
    public class City
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string CityName { get; set; }
        public float Temp { get; set; }
        public DateTime Hora { get; set; }
        public string PhotoUrl { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}
