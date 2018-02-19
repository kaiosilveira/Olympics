using Olympics.Domain.Contracts.Interfaces;
using Olympics.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Olympics.DataAccess.Repositories
{

    public class CountriesRepository : ICountriesRepository
    {
        public static List<Country> MockedCountryList = new List<Country>()
        {
            new Country() { Id  = 1, Name = "Brasil", GoldMedals = 32, SilverMedals = 57, BronzeMedals = 98 },
            new Country() { Id  = 2, Name = "França", GoldMedals = 36, SilverMedals = 62, BronzeMedals = 112 },
            new Country() { Id  = 3, Name = "EUA", GoldMedals = 45, SilverMedals = 87, BronzeMedals = 132 },
        };

        public int Delete(int id)
        {
            var idx = MockedCountryList.FindIndex(c => c.Id == id);
            MockedCountryList.RemoveAt(idx);
            return 1;
        }

        public Country Get(int id)
        {
            return MockedCountryList.FirstOrDefault(country => country.Id == id);
        }

        public IEnumerable<Country> List()
        {
            return MockedCountryList;
        }

        public int Update(Country country)
        {
            var c = MockedCountryList.Find(ctr => ctr.Id == country.Id);
            c.Name = country.Name;
            c.GoldMedals = country.GoldMedals;
            c.SilverMedals = country.SilverMedals;
            c.BronzeMedals = country.BronzeMedals;
            return 1;
        }

        public int Create(Country country)
        {
            MockedCountryList.Add(country);
            return 1;
        }
    }
}
