using Olympics.DataAccess.Factories;
using Olympics.Domain.Contracts.Enumerators;
using Olympics.Domain.Contracts.Interfaces;
using Olympics.Domain.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using System;
using Olympics.DataAccess.Repositories.Abstractions;

namespace Olympics.DataAccess.Repositories
{ 
    public class CountriesRepository : Repository, ICountriesRepository
    {
        public static List<Country> MockedCountryList = new List<Country>()
        {
            new Country() { Id  = 1, Name = "Brasil", GoldMedals = 32, SilverMedals = 57, BronzeMedals = 98 },
            new Country() { Id  = 2, Name = "França", GoldMedals = 36, SilverMedals = 62, BronzeMedals = 112 },
            new Country() { Id  = 3, Name = "EUA", GoldMedals = 45, SilverMedals = 87, BronzeMedals = 132 },
        };

        public Country Get(int id)
        {
            string query = "SELECT * FROM Country WHERE Id = @Id";
            var parameters = new { Id = id };
            return base.QuerySingle<Country>(query, parameters);
        }

        public IEnumerable<Country> List()
        {
            string sql = "SELECT * FROM Country";
            return base.Query<Country>(sql);
        }

        public int Delete(int id)
        {
            string query = @"
                DELETE FROM Country WHERE Id = @Id
                SELECT @@ROWCOUNT";

            return base.QuerySingle<int>(query, new { Id = id });
        }

        public int Update(Country country)
        {
            string query = @"
                UPDATE Country
                SET 
                    Name = @Name,
                    GoldMedals = @GoldMedals,
                    SilverMedals = @SilverMedals,
                    BronzeMedals = @BronzeMedals
                WHERE Id = @Id
                
                SELECT @@ROWCOUNT
            ";

            var parameters = new
            {
                Id = country.Id,
                Name = country.Name,
                GoldMedals = country.GoldMedals,
                SilverMedals = country.SilverMedals,
                BronzeMedals = country.BronzeMedals,
            };

            return base.QuerySingle<int>(query, parameters);

        }

        public int Create(Country country)
        {
            string query = @"
                INSERT INTO Country(Name, GoldMedals, SilverMedals, BronzeMedals) 
                VALUES(@Name, @GoldMedals, @SilverMedals, @BronzeMedals)
                SELECT @@ROWCOUNT
            ";

            var parameters = new
            {
                Name = country.Name,
                GoldMedals = country.GoldMedals,
                SilverMedals = country.SilverMedals,
                BronzeMedals = country.BronzeMedals
            };

            return base.QuerySingle<int>(query, parameters);
        }
    }
}
