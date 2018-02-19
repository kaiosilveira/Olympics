using Olympics.Domain.Entities;
using System.Collections;
using System.Collections.Generic;

namespace Olympics.Tests.Unit.Countries.Data
{
    public class CountryPostTestData_Ok : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] { new Country() { Id = 1, Name = "Brasil", GoldMedals = 1, SilverMedals = 1, BronzeMedals = 1 } },
            new object[] { new Country() }
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

}
