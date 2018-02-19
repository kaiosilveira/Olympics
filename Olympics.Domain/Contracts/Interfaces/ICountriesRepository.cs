using Olympics.Domain.Entities;
using System.Collections.Generic;

namespace Olympics.Domain.Contracts.Interfaces
{
    public interface ICountriesRepository
    {
        IEnumerable<Country> List();
        Country Get(int id);
        int Update(Country country);
        int Delete(int id);
        int Create(Country country);
    }
}
