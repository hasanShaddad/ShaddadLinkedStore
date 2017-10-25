namespace Elmatgar.Core.Models
{
    public partial class Adress  
    {
        
        private Countries _countries;

        private Cities _cities;
        private Zones _zones;
        public Adress(Countries countries,Cities cities, Zones zones)
        {
            _countries = countries;
            _cities = cities;
            _zones = zones;
        }

      
    }
}
